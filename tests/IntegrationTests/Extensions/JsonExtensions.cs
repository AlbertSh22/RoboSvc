using System.Text.Json;

namespace IntegrationTests.Extensions
{
    /// <summary>
    ///     Extends functionality to serialize value types to JSON and to deserialize 
    ///     JSON into value types.
    /// </summary>
    internal static class JsonExtensions
    {
        #region Fields

        private static readonly JsonSerializerOptions _jsonOptions =
            new()
            {
                PropertyNameCaseInsensitive = true
            };

        #endregion

        #region Methods

        /// <summary>
        ///     Parses the text a single JSON value into an instance of the target type.
        /// </summary>
        /// <typeparam name="T">
        ///     The target type of the JSON value.
        /// </typeparam>
        /// <param name="json">
        ///     The JSON text to parse.
        /// </param>
        /// <returns>
        ///     A T representation of the JSON value.
        /// </returns>
        internal static T? FromJson<T>(this string json) =>
            JsonSerializer.Deserialize<T>(json, _jsonOptions);

        /// <summary>
        ///     Converts the value of a type specified by a generic type parameter into a JSON string.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the value to serialize.
        /// </typeparam>
        /// <param name="obj">
        ///     The value to convert.
        /// </param>
        /// <returns>
        ///     A JSON representation of the value.
        /// </returns>
        internal static string ToJson<T>(this T obj) =>
            JsonSerializer.Serialize(obj, _jsonOptions);

        #endregion
    }
}
