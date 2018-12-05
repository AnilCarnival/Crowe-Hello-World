
namespace Crowe.ConsoleApplication
{
    using System;
    using CroweHelloWorldFoundation.FrameworkWrappers;
    using CroweHelloWorldFoundation.Models;
    using CroweHelloWorldFoundation.Resources;
    using CroweHelloWorldFoundation.Services;
    using RestSharp;

    /// <summary>
    ///     Service class for communicating with the Hello World Web API
    /// </summary>
    public class WebService : IWebService
    {
        /// <summary>
        ///     The application settings service
        /// </summary>
        private readonly IAppSettings appSettings;

        /// <summary>
        ///     The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        ///     The Rest client
        /// </summary>
        private readonly IRestClient restClient;

        /// <summary>
        ///     The Rest request
        /// </summary>
        private readonly IRestRequest restRequest;

        /// <summary>
        ///     The wrapped Uri service
        /// </summary>
        private readonly IUri uriService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WebService" /> class.
        /// </summary>
        /// <param name="restClient">The rest client</param>
        /// <param name="restRequest">The rest request</param>
        /// <param name="appSettings">The application settings</param>
        /// <param name="uriService">The uri service</param>
        /// <param name="logger">The logger</param>
        public WebService(
            IRestClient restClient,
            IRestRequest restRequest,
            IAppSettings appSettings,
            IUri uriService,
            ILogger logger)
        {
            this.restClient = restClient;
            this.restRequest = restRequest;
            this.appSettings = appSettings;
            this.uriService = uriService;
            this.logger = logger;
        }

        /// <summary>
        ///     Gets HelloWorldData from the web API
        /// </summary>
        /// <returns>A HelloWorldData mode</returns>
        public HellowWorldData GetHelloWorldContent()
        {
            HellowWorldData hellowWorldData = null;

            // Set the URL for the request
            this.restClient.BaseUrl = this.uriService.GetUri(this.appSettings.Get(AppSettingsKeys.ApiUrlKey));

            // Setup the request
            this.restRequest.Resource = "HelloWorld";
            this.restRequest.Method = Method.GET;

            // Clear the request parameters
            this.restRequest.Parameters.Clear();

            // Execute the call and get the response
            var hellowWorldDataResponse = this.restClient.Execute<HellowWorldData>(this.restRequest);

            // Check for data in the response
            if (hellowWorldDataResponse != null)
            {
                // Check if any actual data was returned
                if (hellowWorldDataResponse.Data != null)
                {
                    hellowWorldData = hellowWorldDataResponse.Data;
                }
                else
                {
                    var errorMessage = "Error in RestSharp" + " Error: "
                                       + hellowWorldDataResponse.ErrorMessage + " HTTP Status Code: "
                                       + hellowWorldDataResponse.StatusCode + " HTTP Status Description: "
                                       + hellowWorldDataResponse.StatusDescription;

                    // existing exception
                    if (hellowWorldDataResponse.ErrorMessage != null && hellowWorldDataResponse.ErrorException != null)
                    {
                        // Log an  exception
                        this.logger.Error(errorMessage, null, hellowWorldDataResponse.ErrorException);
                    }
                    else
                    {
                        // Log an informative exception including the RestSharp content
                        this.logger.Error(errorMessage, null, new Exception(hellowWorldDataResponse.Content));
                    }
                }
            }
            else
            {
                // Log the exception
                const string ErrorMessage =
                    "Did not get any response from the Hello World Web Api for the Method: GET /hellowWorldData";

                this.logger.Error(ErrorMessage, null, new Exception(ErrorMessage));
            }

            return hellowWorldData;
        }
    }
}