
namespace CroweHelloWorldWeb
{
    using System.Web.Http;

    using CroweHelloWorldFoundation.FrameworkWrappers;
    using CroweHelloWorldFoundation.Mappers;
    using CroweHelloWorldFoundation.Services;
    using LightInject;

    /// <summary>
    /// Configures dependency injection via LightInject
    /// </summary>
    public static class LightInjectConfig
    {
        /// <summary>
        /// Registers main components
        /// </summary>
        /// <param name="config">Http Configuration</param>
        public static void Register(HttpConfiguration config)
        {
            var container = new ServiceContainer();
            container.RegisterApiControllers();        

            container.EnablePerWebRequestScope();
            container.EnableWebApi(GlobalConfiguration.Configuration);
            container.EnableMvc();

            // Register Services
            RegisterServices(container);
        }

        /// <summary>
        /// Registers the dependency services to be injected
        /// </summary>
        /// <param name="serviceRegistry">The Service Registry</param>
        private static void RegisterServices(IServiceRegistry serviceRegistry)
        {
            // Register default Application Settings Service
            serviceRegistry.Register<IAppSettings, ConfigAppSettings>();

            // Register default Logger Service
            ////serviceRegistry.Register<ILogger, JsonL4NLogger>();
            serviceRegistry.RegisterInstance(typeof(ILogger), new JsonL4NLogger());

            // Register default Hosting Environment Service
            serviceRegistry.Register<IHostingEnvironmentService, ServerHostingEnvironmentService>();

            // Register default File IO Service
            serviceRegistry.Register<IFileIOService, TextFileIOService>();

            // Register default Data Service
            serviceRegistry.Register<IHelloWorldContentService, HelloWorldContentService>();

            // Register default Hello World mapper
            serviceRegistry.Register<IDataMapper, DataMapper>();
        }
    }
}
