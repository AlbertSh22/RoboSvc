using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

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
    /// <typeparam name="TViewModel">
    ///     The view model type to pass information to the controller.
    /// </typeparam>
    [Controller]
    public abstract class ControllerCrud<TEntity, TId, TViewModel> : 
        ControllerBase
        where TEntity : class, IEntity<TId>
        where TId : struct
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IGenericRepository<TEntity, TId> _repo;

        #endregion

        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the CRUD controller.
        /// </summary>
        /// <param name="repo">
        ///     The instance of the generic repository.
        /// </param>
        /// <param name="mapper">
        ///     The Mapper.AutoMapper object of the AutoMapper library used to map data 
        ///     from one object to another.
        /// </param>
        protected ControllerCrud(IGenericRepository<TEntity, TId> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        /// <summary>
        ///     Returns the belonging List for the TViewModel.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous read operation. The task result contains the 
        ///     created OkObjectResult object for the response if TEntity items returned successfully.
        /// </returns>
        protected virtual async Task<ActionResult<IEnumerable<TViewModel>>> GetItemsAsync()
        {
            var result = await _repo.GetAll().ToListAsync();

            var resultVM = _mapper.Map<IEnumerable<TViewModel>>(result);
            
            return Ok(resultVM);
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
        ///     otherwise, NotFoundResult object if none found.
        /// </returns>
        protected virtual async Task<IActionResult> GetItemAsync(TId id)
        { 
            var item = await _repo.GetByIdAsync(id);

            if (item is null)
            { 
                return NotFound();
            }

            var itemVM = _mapper.Map<TViewModel>(item);

            return Ok(itemVM);
        }

        /// <summary>
        ///     Updates a given entity.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the TEntity item.
        /// </param>
        /// <param name="itemVM">
        ///     The given entity which will be updated into database.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous update action. The task result contains the 
        ///     created NoContentResult object for the response if TEntity item was updated 
        ///     successfully, or BadRequestResult object for bad request, or NotFoundResult object if 
        ///     none found.
        /// </returns>
        protected virtual async Task<IActionResult> PutItemAsync(TId id, TViewModel itemVM)
        {
            var item = _mapper.Map<TEntity>(itemVM);
            
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
        /// <param name="itemVM">
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
        protected virtual async Task<IActionResult> CreateItemAsync(TViewModel itemVM, 
            string? actionName)
        {
            var item = _mapper.Map<TEntity>(itemVM);

            await _repo.CreateAsync(item);

            var createdItemVM = _mapper.Map<TViewModel>(item);

            return CreatedAtAction(
                actionName,
                new { id = item.Id },
                createdItemVM);
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
