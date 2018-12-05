
namespace Crowe.ConsoleApplication
{
    using CroweHelloWorldFoundation.Services;

    /// <summary>
    ///    Crowe Hello World Console 
    /// </summary>
    public class CroweHelloWorldConsole : ICroweHelloWorldConsole
    {
        /// <summary>
        ///     The Hello World Web Service
        /// </summary>
        private readonly IWebService helloWorldWebService;

        /// <summary>
        ///     The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CroweHelloWorldConsole" /> class.
        /// </summary>
        /// <param name="helloWorldWebService">The IoC dependency injected hello world web service</param>
        /// <param name="logger">The logger</param>
        public CroweHelloWorldConsole(IWebService helloWorldWebService, ILogger logger)
        {
            this.helloWorldWebService = helloWorldWebService;
            this.logger = logger;
        }

        /// <summary>
        ///     Runs the main Hello World Console Application
        /// </summary>
        /// <param name="arguments">The command line arguments.</param>
        public void Run(string[] arguments)
        {
            // Get helloWorldData
            var helloWorldData = this.helloWorldWebService.GetHelloWorldContent();

            // Write helloWorld Data to the console screen
            this.logger.Info(helloWorldData != null ? helloWorldData.Content : "No data returned", null);
        }
    }
}