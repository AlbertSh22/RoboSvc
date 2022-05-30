using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using EntityDal.Interfaces;
using DomainLogic.Interfaces;

namespace PublicApi.Controllers
{
    /// <summary>
    ///     A generic controller for CRUD actions on a repository for a specific type.
    /// </summary>
    /// <typeparam name="TId">
    ///     The type of the ID of the entity the repository manages.
    /// </typeparam>
    /// <typeparam name="TEntity">
    ///     The domain type the repository manages.
    /// </typeparam>
    [Controller]
    public abstract class ControllerCrud<TEntity, TId> : 
        ControllerBase
        where TEntity : class, IEntity<TId>
        where TId : struct
    {
        #region Fields

        private readonly IGenericRepository<TEntity, TId> _repo;

        #endregion

        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the CRUD controller.
        /// </summary>
        /// <param name="repo">
        ///     The instance of the generic repository.
        /// </param>
        protected ControllerCrud(IGenericRepository<TEntity, TId> repo)
        {
            _repo = repo;
        }

        #endregion

        #region Actions

        /// <summary>
        ///     Returns the belonging List for the TEntity.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous read operation. The task result contains the 
        ///     created OkObjectResult object for the response if TEntity items returned successfully.
        /// </returns>
        protected virtual async Task<ActionResult<IEnumerable<TEntity>>> GetItemsAsync()
        {
            var result = await _repo.GetAll().ToListAsync();
            
            return Ok(result);
        }

        /// <summary>
        ///     Get the data for given ID and find the item with this unique ID.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the TEntity item.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous read operation. The task result contains the 
        ///     created OkObjectResult object for the response if an entity with the specified ID found; 
        ///     otherwise, or NotFoundResult object if none found.
        /// </returns>
        protected virtual async Task<IActionResult> GetItemAsync(TId id)
        { 
            var item = await _repo.GetByIdAsync(id);

            if (item is null)
            { 
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        ///     Updates a given entity.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the TEntity item.
        /// </param>
        /// <param name="item">
        ///     The given entity which will be updated into database.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous update action. The task result contains the 
        ///     created NoContentResult object for the response if TEntity item was updated 
        ///     successfully, or BadRequestResult object for bad request, or NotFoundResult object if 
        ///     none found.
        /// </returns>
        protected virtual async Task<IActionResult> PutItemAsync(TId id, TEntity item)
        {
            if (!id.Equals(item.Id))
            {
                return BadRequest();
            }

            try
            {
                await _repo.UpdateAsync(item);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        /// <summary>
        ///     Creates the given entity.
        /// </summary>
        /// <param name="item">
        ///     The given entity which will be inserted into database.
        /// </param>
        /// <param name="actionName">
        ///     The name of the action to use for generating the URL.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous insert action. The task result contains the 
        ///     created CreatedAtActionResult object for the response if TEntity item was inserted 
        ///     successfully.
        /// </returns>
        protected virtual async Task<IActionResult> CreateItemAsync(TEntity item, 
            string? actionName)
        {
            await _repo.CreateAsync(item);

            return CreatedAtAction(
                actionName,
                new { id = item.Id },
                item
                );
        }

        /// <summary>
        ///     Removes the entity with the given ID.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the TEntity item.   
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous delete action. The task result contains the 
        ///     created NotFoundResult object for the response if none found,
        ///     NoContentResult object if TEntity item was deleted.
        /// </returns>
        protected virtual async Task<IActionResult> DeleteItemAsync(TId id)
        { 
            var item = await _repo.GetByIdAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            await _repo.DeleteAsync(id);

            return NoContent();
        }

        #endregion
    }
}
