using System.Text;
using System.Net.Mime;
using System.Net.Http.Headers;

using EntityDal.Interfaces;

namespace IntegrationTests.Extensions
{
    /// <summary>
    ///     A class that extends the functionality of the existing HttpClient class 
    ///     to verify the behavior of the class inherited from a ControllerCrud.
    /// </summary>
    internal static class ClientExtensions
    {
        #region Consts

        private const string EntityFormat = "api/{0}";
        private const string IdFormat = "api/{0}/{1}";

        #endregion

        #region Internals

        private static async Task<IEnumerable<TEntity>?> ReadAllInternalAsync<TEntity>(
            this HttpClient client, string format, string controllerName)
        {
            var requestUri = string.Format(format, controllerName);

            using var response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<IEnumerable<TEntity>>();

            return model;
        }

        private static async Task<string?> ReadJsonByIdInternalAsync<TId, TEntity>(
            this HttpClient client, string format, string controllerName, TId id)
        {
            var requestUri = string.Format(format, controllerName, id);

            using var response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();

            return stringResponse;
        }

        #region Unused

        //private static async Task<TEntity?> ReadByIdInternalAsync<TId, TEntity>(
        //    this HttpClient client, string format, string controllerName, TId id)
        //{
        //    var stringResponse = await client.ReadJsonByIdInternalAsync<TId, TEntity>(format, controllerName, id);

        //    Assert.IsNotNull(stringResponse);

        //    var model = stringResponse.FromJson<TResponse>();

        //    return model;
        //}

        #endregion

        private static async Task<TEntity?> CreateInternalAsync<TEntity>(
            this HttpClient client, string format, string controllerName, TEntity content)
        {
            var requestUri = string.Format(format, controllerName);

            var serialized = new StringContent(content.ToJson(),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            using var response = await client.PostAsync(requestUri, serialized);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<TEntity>();

            return model;
        }

        private static async Task UpdateInternalAsync<TId, TEntity>(
            this HttpClient client, string format, string controllerName, TId id, TEntity content)
        {
            var requestUri = string.Format(format, controllerName, id);

            var serialized = new StringContent(content.ToJson(),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            using var response = await client.PutAsync(requestUri, serialized);

            response.EnsureSuccessStatusCode();
        }

        private static async Task DeleteInternalAsync<TId>(
            this HttpClient client, string format, string controllerName, TId id)
        {
            var requestUri = string.Format(format, controllerName, id);

            using var response = await client.DeleteAsync(requestUri);

            response.EnsureSuccessStatusCode();
        }

        #endregion

        #region Public

        /// <summary>
        ///     A function that tests the generic CRUD actions.
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The given entity which will be tested.
        /// </typeparam>
        /// <typeparam name="TId">
        ///     The ID of the given entity.
        /// </typeparam>
        /// <param name="client">
        ///     An instance of the class for sending HTTP requests and 
        ///     receiving HTTP responses.
        /// </param>
        /// <param name="controllerName">
        ///     The name of the controller for handling HTTP requests/responses.
        /// </param>
        /// <param name="fnCreate">
        ///     A function that is used to create a new entity item using 
        ///     an existing item as a template.
        /// </param>
        /// <param name="fnEdit">
        ///     A function that is used to update an existing entity item.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous CRUD test operations.
        /// </returns>
        public static async Task TestGenericRepository<TEntity, TId>(
            this HttpClient client, string controllerName, 
            Func<string, string> fnCreate, Func<string, string> fnEdit)
            where TEntity : class, IEntity<TId>
            where TId : struct, IComparable<TId>
        {
            var model = await client.ReadAllInternalAsync<TEntity>(EntityFormat, controllerName);

            Assert.IsNotNull(model);
            Assert.IsTrue(model.Any());

            var maxId = model.Max(x => x.Id);

            Assert.IsNotNull(maxId);

            var json = await client.ReadJsonByIdInternalAsync<TId, TEntity>(IdFormat, controllerName, maxId);

            Assert.IsNotNull(json);

            var newEntity = fnCreate(json).FromJson<TEntity>();

            Assert.IsNotNull(newEntity);

            var createdEntity = await client.CreateInternalAsync(EntityFormat, controllerName, newEntity);

            Assert.IsNotNull(createdEntity);
            Assert.IsTrue(maxId.CompareTo(createdEntity.Id) < 0);

            json = await client.ReadJsonByIdInternalAsync<TId, TEntity>(IdFormat, controllerName, createdEntity.Id);

            Assert.IsNotNull(json);

            var editedEntity = fnEdit(json).FromJson<TEntity>();

            Assert.IsNotNull(editedEntity);

            await client.UpdateInternalAsync(IdFormat, controllerName, editedEntity.Id, editedEntity);

            await client.DeleteInternalAsync(IdFormat, controllerName, editedEntity.Id);
        }

        #endregion
    }
}
