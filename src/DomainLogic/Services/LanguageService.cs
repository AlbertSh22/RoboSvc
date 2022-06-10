using EntityDal.Models;
using EntityDal.Context;

namespace DomainLogic.Services
{
    using Queries;
    using Interfaces;

    /// <summary>
    ///     Inherits a GenericRepository class to the Language class 
    ///     that it will be handle.
    /// </summary>
    public class LanguageService : GenericRepository<Language, short>,
        ILanguageService
    {
        #region Ctor

        /// <summary>
        ///     Calls the base constructor to remember an instance of DB context.
        /// </summary>
        /// <param name="dbContext">
        ///     The instance of the DB Context.
        /// </param>
        public LanguageService(RoboSvcContext dbContext) : base(dbContext) 
        { 
        }

        #endregion

        #region Implementation

        /// <summary>
        ///     Gets a bool value that indicates whether the given ID and 
        ///     value pair is unique or not.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the member to validate.
        /// </param>
        /// <param name="propertyValue">
        ///     The value of the member to validate.
        /// </param>
        /// <param name="idName">
        ///     The name of the object ID to validate.
        /// </param>
        /// <param name="idValue">
        ///     The value of the object ID to validate.
        /// </param>
        /// <returns>
        ///     Returns true is a unique constraint applies to the given 
        ///     ID and value pair; otherwise, false.
        /// </returns>
        public bool IsUnique(string propertyName, object? propertyValue,
            string idName, object? idValue)
        {
            var query = GetAll().IsUnique(propertyName, propertyValue, idName, idValue);

            return !query.Any();
        }

        #endregion
    }
}
