using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;

using EntityDal.Models;
using DomainLogic.Interfaces;

namespace PublicApi.Controllers
{
    /// <summary>
    ///     A controller using Language entity in the base class.
    /// </summary>
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class LanguagesController : ControllerCrud<Language, short>
    {
        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the Languages controller.
        /// </summary>
        /// <param name="service">
        ///     The instance of the Language service.
        /// </param>
        public LanguagesController(ILanguageService service)
            : base(service)
        {
        }

        #endregion

        #region Actions

        /// <summary>
        ///     Returns all instances of the Language.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous read action. The task result contains the 
        ///     created OkObjectResult object for the response if Language items returned successfully.
        /// </returns>
        /// <response code="200">
        ///     Returns a list of Language items.
        /// </response>
        // GET: api/Languages
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Language>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
            return await GetItemsAsync();
        }

        /// <summary>
        ///     Retrieves a Language item by its ID.
        /// </summary>
        /// <param name="id">
        ///     The ID of the Language item.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous read action. The task result contains the 
        ///     created OkObjectResult object for response if Language item is found by its id, 
        ///     or NotFoundResult object if none found.
        /// </returns>
        /// <response code="200">
        ///     Returns the Language item with the given ID.
        /// </response>
        /// <response code="404">
        ///     If Language item is not exists.
        /// </response>
        // GET: api/Languages/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Language), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLanguage(short id)
        { 
            return await GetItemAsync(id);
        }

        /// <summary>
        ///     Updates an existing Language item by its ID.
        /// </summary>
        /// <param name="id">
        ///     Language item ID.
        /// </param>
        /// <param name="item">
        ///     The Language entity which will be updated into database.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous update action. The task result contains the 
        ///     created NoContentResult object for the response if Language item was updated 
        ///     successfully, BadRequestResult object for bad request, or NotFoundResult object if 
        ///     none found.
        /// </returns>
        /// <response code="204">
        ///     If Language item was updated successfully.
        /// </response>
        /// <response code="400">
        ///     For bad request.
        /// </response>
        /// <response code="404">
        ///     If nothing was found.
        /// </response>
        // PUT: api/Languages/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutLanguage(short id, Language item)
        {
            return await PutItemAsync(id, item);
        }

        /// <summary>
        ///     Creates a new Language item.
        /// </summary>
        /// <param name="item">
        ///     The Language entity which will be inserted into database.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous create action. The task result contains the 
        ///     created CreatedAtActionResult object for the response if Language item was inserted 
        ///     successfully.
        /// </returns>
        /// <response code="201">
        ///     If Language item was created successfully.
        /// </response>
        // POST: api/Languages
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostLanguage(Language item)
        {
            return await CreateItemAsync(item, nameof(GetLanguage));
        }

        /// <summary>
        ///     Delete an existing Language item.
        /// </summary>
        /// <param name="id">
        ///     Language item ID.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous delete action. The task result contains the 
        ///     created NoContentResult object for the response if Language item was removed successfully, 
        ///     or NotFoundResult object if none found.
        /// </returns>
        /// <response code="204">
        ///     If Language item was deleted successfully.
        /// </response>
        /// <response code="404">
        ///     If nothing was found.
        /// </response>
        // DELETE: api/Languages/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLanguage(short id)
        { 
            return await DeleteItemAsync(id);
        }

        #endregion
    }
}
