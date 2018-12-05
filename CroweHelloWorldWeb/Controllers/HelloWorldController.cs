
namespace CroweHelloWorldWeb.Controllers
{
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Web.Http;
    using CroweHelloWorldFoundation.Attributes;
    using CroweHelloWorldFoundation.Models;
    using CroweHelloWorldFoundation.Services;

    /// <summary>
    ///     API controller for getting and setting HelloWorld data value.
    /// </summary>
    [WebApiExceptionFilter]
    public class HelloWorldController : ApiController
    {
        /// <summary>
        ///     The data service
        /// </summary>
        private readonly IHelloWorldContentService dataService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelloWorldController" /> class.
        /// </summary>
        /// <param name="dataService">The injected data service</param>
        public HelloWorldController(IHelloWorldContentService dataService)
        {
            this.dataService = dataService;
        }

        /// <summary>
        ///     HelloWorld data
        /// </summary>
        /// <returns>HelloWorld data model</returns>
        [WebApiExceptionFilter(Type = typeof(IOException), Status = HttpStatusCode.ServiceUnavailable, Severity = SeverityCode.Error)]
        [WebApiExceptionFilter(Type = typeof(SettingsPropertyNotFoundException), Status = HttpStatusCode.ServiceUnavailable, Severity = SeverityCode.Error)]
        public HellowWorldData Get()
        {
            return this.dataService.GetHelloWorldContent();
        }
    }
}