using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using EntityDal.Models;
using DomainLogic.Interfaces;

namespace PublicApi.Controllers
{
    using Models.Classifiers;

    /// <summary>
    ///     A controller using Language and LanguageViewModel entities in the base class.
    /// </summary>
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class LanguagesController : 
        ControllerCrud<Language, short, LanguageViewModel>
    {
        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the Languages controller.
        /// </summary>
        /// <param name="service">
        ///     The instance of the Language service.
        /// </param>
        /// <param name="mapper"> 
        ///     The Mapper.AutoMapper object of the AutoMapper library used to map data
        ///     from one object to another.
        /// </param>
        public LanguagesController(ILanguageService service, 
            IMapper mapper) : base(service, mapper)
        {
        }

        #endregion

        #region Actions

        /// <summary>
        ///     Returns all instances of the LanguageViewModel.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous read action. The task result contains the 
        ///     created OkObjectResult object for the response if Language items returned successfully.
        /// </returns>
        /// <response code="200">
        ///     Returns a list of LanguageViewModel items.
        /// </response>
        // GET: api/Languages
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LanguageViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LanguageViewModel>>> GetLanguages()
        {
            return await GetItemsAsync();
        }

        /// <summary>
        ///     Retrieves a LanguageViewModel item by its ID.
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
        ///     Returns the LanguageViewModel item with the given ID.
        /// </response>
        /// <response code="404">
        ///     If LanguageViewModel item is not exists.
        /// </response>
        // GET: api/Languages/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LanguageViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLanguage(short id)
        { 
            return await GetItemAsync(id);
        }

        /// <summary>
        ///     Updates an existing LanguageViewModel item by its ID.
        /// </summary>
        /// <param name="id">
        ///     LanguageViewModel item ID.
        /// </param>
        /// <param name="itemVM">
        ///     The LanguageViewModel entity which will be updated into database.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous update action. The task result contains the 
        ///     created NoContentResult object for the response if Language item was updated 
        ///     successfully, BadRequestResult object for bad request, or NotFoundResult object if 
        ///     none found.
        /// </returns>
        /// <response code="204">
        ///     If LanguageViewModel item was updated successfully.
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
        public async Task<IActionResult> PutLanguage(short id, LanguageViewModel itemVM)
        {
            return await PutItemAsync(id, itemVM);
        }

        /// <summary>
        ///     Creates a new LanguageViewModel item.
        /// </summary>
        /// <param name="itemVM">
        ///     The LanguageViewModel entity which will be inserted into database.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous create action. The task result contains the 
        ///     created CreatedAtActionResult object for the response if Language item was inserted 
        ///     successfully.
        /// </returns>
        /// <response code="201">
        ///     If LanguageViewModel item was created successfully.
        /// </response>
        /// <response code="400">
        ///     For bad request.
        /// </response>
        // POST: api/Languages
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostLanguage(LanguageViewModel itemVM)
        {
            return await CreateItemAsync(itemVM, nameof(GetLanguage));
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
