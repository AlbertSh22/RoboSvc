using Microsoft.EntityFrameworkCore;

using EntityDal.Context;
using EntityDal.Interfaces;

using DomainLogic.Queries;

namespace DomainLogic.Services
{
    using Interfaces;
    using Interfaces.Validation;

    /// <summary>
    ///     Implements generic repository pattern.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The domain type the repository manages.
    /// </typeparam>
    /// <typeparam name="TId">
    ///     The data type of the ID of the entity.
    /// </typeparam>
    public class GenericRepository<TEntity, TId> :
        IGenericRepository<TEntity, TId>, IUniqueConstraint
        where TEntity : class, IEntity<TId>
        where TId : struct
    {
        #region Fields

        private readonly RoboSvcContext _dbContext;

        #endregion

        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the generic repository.
        /// </summary>
        /// <param name="dbContext">
        ///     The instance of the DB Context.
        /// </param>
        public GenericRepository(RoboSvcContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns the belonging DbSet for the Entity.
        /// </summary>
        /// <returns>
        ///     The IQueryable to use to further process the query.
        /// </returns>
        public IQueryable<TEntity> GetAll()
        { 
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        /// <summary>
        ///     Gets the data for given ID and finds the entity with this unique ID.
        /// </summary>
        /// <param name="id">
        ///     The ID of the entity the repository manages.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous find operation. 
        ///     The task result contains found entity with the given ID or NULL if none found.
        /// </returns>
        public async Task<TEntity?> GetByIdAsync(TId id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity is null)
                return entity;

            _dbContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        /// <summary>
        ///     Creates a given entity.
        /// </summary>
        /// <param name="entity">
        ///     The given entity which will be inserted into database.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous create operation. The task result contains the 
        ///     number of state entries written to the database.
        /// </returns>
        public async Task<int> CreateAsync(TEntity entity)
        { 
            await _dbContext.Set<TEntity>().AddAsync(entity);

            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        ///     Updates a given entity.
        /// </summary>
        /// <param name="entity">
        ///     The given entity which will be updated into database.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous update operation. The task result contains the 
        ///     number of state entries written to the database.
        /// </returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        ///     Removes the entity with the given ID.
        /// </summary>
        /// <param name="id">
        ///     The ID of the entity the repository manages.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous delete operation. The task result contains the 
        ///     number of state entries written to the database.
        /// </returns>
        public async Task<int> DeleteAsync(TId id)
        { 
            var entity = await GetByIdAsync(id);

            if (entity is not null)
            { 
                _dbContext.Set<TEntity>().Remove(entity);

                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        /// <summary>
        ///     Determines whether an entity with the specified ID exists.
        /// </summary>
        /// <param name="id">
        ///     The ID of the entity.
        /// </param>
        /// <returns>
        ///     True if an entity with the specified ID found; otherwise, false.     
        /// </returns>
        public async Task<bool> ExistsAsync(TId id)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(x =>
                id.Equals(x.Id));
        }

        #region Constrains

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
        ///    The name of the object ID to validate.
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

        #endregion
    }
}
