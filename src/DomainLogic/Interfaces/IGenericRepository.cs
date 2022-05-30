using EntityDal.Interfaces;

namespace DomainLogic.Interfaces
{
    /// <summary>
    ///     Interface for generic CRUD operations on a repository for a specific type.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type in database.
    /// </typeparam>
    /// <typeparam name="TId">
    ///     The entity ID type.
    /// </typeparam>
    public interface IGenericRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        /// <summary>
        ///     Returns the belonging DbSet for the Entity.
        /// </summary>
        /// <returns>
        ///     The IQueryable to use to further process the query.
        /// </returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        ///     Gets the data for given ID and finds the entity with this unique ID.
        /// </summary>
        /// <param name="id">
        ///     The entity ID.
        /// </param>
        /// <returns>
        ///     The entity with the given ID or NULL if none found.
        /// </returns>
        Task<TEntity?> GetByIdAsync(TId id);

        /// <summary>
        ///     Creates the given entity.
        /// </summary>
        /// <param name="entity">
        ///     The given entity which will be inserted into database.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous create operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        Task<int> CreateAsync(TEntity entity);

        /// <summary>
        ///     Updates a given entity.
        /// </summary>
        /// <param name="entity">
        ///     The given entity which will be updated into database.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        ///     Removes the entity with the given ID.
        /// </summary>
        /// <param name="id">
        ///     The entity ID.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous delete operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        Task<int> DeleteAsync(TId id);

        /// <summary>
        ///     Determines whether an entity with the specified ID exists.
        /// </summary>
        /// <param name="id">
        ///     The entity ID.
        /// </param>
        /// <returns>
        ///     True if an entity with the specified ID found; otherwise, false.
        /// </returns>
        Task<bool> ExistsAsync(TId id);
    }
}
