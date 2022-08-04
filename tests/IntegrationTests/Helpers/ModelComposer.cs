using PublicApi.Models.Classifiers;

namespace IntegrationTests.Helpers
{
    using Extensions;

    /// <summary>
    ///     Creates or modifies data models from JSON text.
    /// </summary>
    internal static class ModelComposer
    {
        #region Languages

        /// <summary>
        ///     Modifies the LanguageViewModel object to create a new entity in the 
        ///     database.
        /// </summary>
        /// <param name="json">
        ///     The JSON text to parse.
        /// </param>
        /// <returns>
        ///     A JSON representation of the modified Language data.
        /// </returns>
        internal static string CreateLanguageItem(string json)
        {
            var item = json.FromJson<LanguageViewModel>();

            Assert.IsNotNull(item);

            item.Id = default;
            item.Name += " 2";
            item.Alpha2 = "vo";
            item.DigitalCode = "007";

            var newItem = item.ToJson();

            return newItem;
        }

        /// <summary>
        ///     Modifies the LanguageViewModel object to edit an existing entity 
        ///     in the database.
        /// </summary>
        /// <param name="json">
        ///     The JSON text to parse.
        /// </param>
        /// <returns>
        ///     A JSON representation of the modified LanguageViewModel data.
        /// </returns>
        internal static string EditLanguageItem(string json)
        {
            var item = json.FromJson<LanguageViewModel>();

            Assert.IsNotNull(item);

            item.Name += " 2";

            var newJson = item.ToJson();

            return newJson;
        }

        #endregion
    }
}
