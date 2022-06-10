using System.ComponentModel.DataAnnotations;

using DomainLogic.Interfaces.Validation;

namespace PublicApi.Validation
{
    /// <summary>
    ///     Specifies that a data field value is unique.
    /// </summary>
    public sealed class UniqueAttribute : ValidationAttribute
    {
        #region Fields

        private readonly string _idName;
        private readonly Type _typeService;
        private readonly Type _typeImplementation;

        #endregion

        #region Ctor

        /// <summary>
        ///     Constructs a new instance of this class for the specified 
        ///     type of the service, type of the implementation, and 
        ///     name of the object ID.
        /// </summary>
        /// <param name="typeService">
        ///     The type of the service to check property value 
        ///     for uniqueness.
        /// </param>
        /// <param name="typeImplementation">
        ///     The type of the implementation to check property value 
        ///     for uniqueness.
        /// </param>
        /// <param name="idName">
        ///     The name of the object ID to validate.
        /// </param>
        public UniqueAttribute(Type typeService, Type typeImplementation, 
            string idName)
        {
            _typeService = typeService;
            _typeImplementation = typeImplementation;

            _idName = idName;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Retrieves an error message to associate with a validation control 
        ///     if validation fails.
        /// </summary>
        /// <param name="memberName">
        ///     The name of the member to validate.
        /// </param>
        /// <returns>
        ///     The error message that is associated with the validation control.
        /// </returns>
        private static string GetErrorMessage(string memberName) =>
            $"The {memberName} value must be unique.";

        #endregion

        #region Overrides

        /// <summary>
        ///     Validates the specified value with the Unique validation attribute.
        /// </summary>
        /// <param name="value">
        ///     The value to validate.
        /// </param>
        /// <param name="validationContext">
        ///     The context information about the validation information.
        /// </param>
        /// <returns>
        ///     An instance of the ValidationResult class.     
        /// </returns>
        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            var services = validationContext.GetServices(_typeService);
            var repoSvc = services.First(x => x?.GetType() == _typeImplementation);

            var uniquenessSvc = repoSvc as IUniqueConstraint;

            if (uniquenessSvc is null)
            {
                throw new InvalidOperationException(
                    $"The {_typeImplementation.Name} class does not implement {nameof(IUniqueConstraint)} interface.");
            }

            var name = validationContext.MemberName ?? 
                validationContext.DisplayName;

            var idValue = validationContext
                .ObjectType.GetProperty(_idName)?.GetValue(
                    validationContext.ObjectInstance, null);

            var isUnique = uniquenessSvc.IsUnique(name, value, _idName, idValue);

            if (!isUnique)
            {
                return new ValidationResult(GetErrorMessage(name));
            }

            return ValidationResult.Success;
        }

        #endregion
    }
}
