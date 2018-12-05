
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
        ///     The Hello World Mapper
        /// </summary>
        private readonly IDataMapper _dataMapper;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelloWorldDataService" /> class.
        /// </summary>
        /// <param name="appSettings">The injected application settings service</param>
        /// <param name="helloWorldMapper">The injected Hello World Mapper</param>
        public HelloWorldContentService(
            IDataMapper dataMapper)
        {
            this._dataMapper = dataMapper;
        }

        /// <summary>
        ///     Gets HelloWorldData
        /// </summary>
        /// <returns>HelloWorldData</returns>
        public HellowWorldData GetHelloWorldContent()
        {
          
            //this content can come from Database, file system, config, or third -party apis or services
            // Get the content to display
            string content = "Hellow World!!!";

            // return type
            var helloWorldContent = this._dataMapper.GetHelloWorldContent(content);

            return helloWorldContent;
        }
    }
}