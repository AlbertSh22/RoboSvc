namespace EntityDal.Interfaces
{
    /// <summary>
    ///     The domain type the repository manages.    
    /// </summary>
    /// <typeparam name="T">
    ///     The data type of the ID of the entity the repository manages.
    /// </typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        ///     Gets the ID of the entity. 
        /// </summary>
        T Id { get; }
    }
}
