using System.Reflection;

namespace IntegrationTests.Helpers
{
    using Extensions;

    /// <summary>
    ///     Contructs a MethodInfo object and invokes the TestGenericRepository 
    ///     method.
    /// </summary>
    internal static class GenericComposer
    {
        /// <summary>
        ///     Invokes the TestGenericRepository method represent by the current instance, 
        ///     the specified parameters. 
        /// </summary>
        /// <param name="controllerName">
        ///     The name of the controller handling HTTP requests/responses.
        /// </param>
        /// <param name="typeArguments">
        ///     A list of types to be substituted for the type parameters of the TestGenericRepository 
        ///     generic method definition.
        /// </param>
        /// <param name="fnCreate">
        ///     A function that is used to create a new entity item using 
        ///     an existing item as a template.
        /// </param>
        /// <param name="fnEdit">
        ///     A function that is used to update an existing entity item.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous method invocation.
        /// </returns>
        internal static async Task InvokeTestGenericRepository(string controllerName, List<Type> typeArguments,
            Func<string, string> fnCreate, Func<string, string> fnEdit)
        {
            var client = ProgramTest.WebClient;

            var miConstructed = BuildTestFuncFor(typeArguments);

            Assert.IsNotNull(miConstructed);

            var result = miConstructed.Invoke(
                null,
                new object[] { client, controllerName, fnCreate, fnEdit }
            );

            Assert.IsNotNull(result);

            await (Task)result;
        }

        /// <summary>
        ///     Substitutes the elements of an array of types for the type parameters of the 
        ///     TestGenericRepository generic methods definition, and returns MethodInfo 
        ///     object representing the resulting constructed method.
        /// </summary>
        /// <param name="typeArguments">
        ///     A list of types to be substituted for the type parameters of the TestGenericRepository 
        ///     generic method definition.
        /// </param>
        /// <returns>
        ///     A MethodInfo object that represents the constructed method 
        ///     formed by substituting the elements of typeArguments for the type parameters 
        ///     of the TestGenericRepository generic method definition.
        /// </returns>
        internal static MethodInfo BuildTestFuncFor(List<Type> typeArguments)
        {
            var mi = typeof(ClientExtensions).GetMethod(
                nameof(ClientExtensions.TestGenericRepository));

            Assert.IsNotNull(mi);

            var miConstructed = mi.MakeGenericMethod(typeArguments.ToArray());

            Assert.IsNotNull(miConstructed);

            return miConstructed;
        }
    }
}
