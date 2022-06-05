using PublicApi.Models.Classifiers;

namespace IntegrationTests.PublicApi
{
    using Helpers;

    /// <summary>
    ///     A test class to verify generic CRUD actions.
    /// </summary>
    [TestClass]
    public class ClassifierTests
    {
        #region Fields

        private readonly List<Tuple<string, Func<string, string>, Func<string, string>, List<Type>>> _dataItems =
            new()
            {
                Tuple.Create(
                    "Languages",
                    ModelComposer.CreateLanguageItem,
                    ModelComposer.EditLanguageItem,
                    new List<Type> { typeof(LanguageViewModel), typeof(short) }
                )
                // etc ...
            };

        #endregion

        #region Tests

        /// <summary>
        ///     A test method to verify generic CRUD actions.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous method invocation 
        ///     to test generic CRUD actions.
        /// </returns>
        [TestMethod]
        public async Task GenericRepositoryTest()
        {
            var client = ProgramTest.WebClient;

            foreach (var item in _dataItems)
            {
                var name = item.Item1;
                var fnCreate = item.Item2;
                var fnEdit = item.Item3;
                var typs = item.Item4;

                await GenericComposer.InvokeTestGenericRepository(name, typs, fnCreate, fnEdit);
            }
        }

        #endregion
    }
}
