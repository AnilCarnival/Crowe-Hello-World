
namespace CroweHelloWorldFoundation.Services
{
    using System.Configuration;
    using CroweHelloWorldFoundation.FrameworkWrappers;
    using CroweHelloWorldFoundation.Mappers;
    using CroweHelloWorldFoundation.Models;
    using CroweHelloWorldFoundation.Resources;

    /// <summary>
    ///     Data service for manipulating Hello World data
    /// </summary>
    public class HelloWorldContentService : IHelloWorldContentService
    {
        /// <summary>
        ///     The application settings service
        /// </summary>
        private readonly IAppSettings appSettings;

        /// <summary>
        ///     The File IO service
        /// </summary>
        private readonly IFileIOService fileIOService;

        /// <summary>
        ///     The Hello World Mapper
        /// </summary>
        private readonly IDataMapper _dataMapper;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelloWorldDataService" /> class.
        /// </summary>
        /// <param name="appSettings">The injected application settings service</param>
        /// <param name="fileIOService">The injected File IO Service</param>
        /// <param name="helloWorldMapper">The injected Hello World Mapper</param>
        public HelloWorldContentService(
            IAppSettings appSettings,
            IFileIOService fileIOService,
            IDataMapper dataMapper)
        {
            this.appSettings = appSettings;
            this.fileIOService = fileIOService;
            this._dataMapper = dataMapper;
        }

        /// <summary>
        ///     Gets HelloWorldData
        /// </summary>
        /// <returns>HelloWorldData</returns>
        public HellowWorldData GetHelloWorldContent()
        {
            // file path
            var filePath = this.appSettings.Get(AppSettingsKeys.HellowWorldFileName);

            if (string.IsNullOrEmpty(filePath))
            {
                // file path not found, throw exception
                throw new SettingsPropertyNotFoundException(
                    ErrorCodes.HelloWorldFileSettingsKeyError, 
                    new SettingsPropertyNotFoundException("The Hellow world file settings name was not found or had no value."));
            }

            // Get the content from the file
            var fileContent = this.fileIOService.ReadFile(filePath);

            // return type
            var helloWorldContent = this._dataMapper.GetHelloWorldContent(fileContent);

            return helloWorldContent;
        }
    }
}