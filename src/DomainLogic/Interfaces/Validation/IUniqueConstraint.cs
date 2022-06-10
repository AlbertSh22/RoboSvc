namespace DomainLogic.Interfaces.Validation
{
    /// <summary>
    ///     Enforces the data integrity in SQL server table.
    /// </summary>
    public interface IUniqueConstraint
    {
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
        ///     Returns true if a unique constraint applies to the given 
        ///     ID and value pair; otherwise, false.
        /// </returns>
        public bool IsUnique(string propertyName, object? propertyValue, 
            string idName, object? idValue);
    }
}
