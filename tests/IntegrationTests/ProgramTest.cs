using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests
{
    /// <summary>
    ///     A test class for bootstrapping an application in memory for 
    ///     functional end to end tests.
    /// </summary>
    [TestClass]
    public class ProgramTest
    {
        #region Fields

        private static WebApplicationFactory<Program> _application;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the global Http client.
        /// </summary>
        public static HttpClient WebClient
        {
            get => _application.CreateClient();
        }

        #endregion

        /// <summary>
        ///     Runs only once before all tests in the solution are run.
        /// </summary>
        /// <param name="_">
        ///     An instance of a TestContext class that is used to store 
        ///     information that is provided to unit tests.
        /// </param>
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext _)
        {
            _application = new WebApplicationFactory<Program>();
        }
    }
}