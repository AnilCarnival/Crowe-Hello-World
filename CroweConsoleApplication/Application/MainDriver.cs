
namespace Crowe.ConsoleApplication
{
    using CroweHelloWorldFoundation.FrameworkWrappers;
    using CroweHelloWorldFoundation.Services;
    using LightInject;
    using RestSharp;

    /// <summary>
    ///     Main class that drives the application
    /// </summary>
    public class MainDriver
    {
        /// <summary>
        ///     Starts the console application with the specified command line arguments
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            // Setup dependency injection and run the application
            using (var container = new ServiceContainer())
            {
                // Configure depenency injection
                container.Register<ICroweHelloWorldConsole, CroweHelloWorldConsole>();
                container.Register<IAppSettings, ConfigAppSettings>();
                container.Register<IConsole, SystemConsole>();
                container.Register<ILogger, ConsoleLogger>();
                container.Register<IUri, SystemUri>();
                container.Register<IWebService, WebService>();
                container.RegisterInstance(typeof(IRestClient), new RestClient());
                container.RegisterInstance(typeof(IRestRequest), new RestRequest());

                // Run the main program
                container.GetInstance<ICroweHelloWorldConsole>().Run(args);
            }
        }
    }
}