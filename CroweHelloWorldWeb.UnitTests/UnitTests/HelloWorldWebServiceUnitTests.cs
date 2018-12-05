
namespace CroweHelloWorldWeb.Tests.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using CroweHelloWorldFoundation.FrameworkWrappers;
    using CroweHelloWorldFoundation.Models;
    using CroweHelloWorldFoundation.Resources;
    using CroweHelloWorldFoundation.Services;
    using Moq;
    using NUnit.Framework;
    using RestSharp;
    using Crowe.ConsoleApplication;

    /// <summary>
    ///     Unit tests for the Hello World Console App
    /// </summary>
    [TestFixture]
    public class HelloWorldWebServiceUnitTests
    {
        /// <summary>
        ///     The list of log messages set by calling classes
        /// </summary>
        private List<string> logMessageList;

        /// <summary>
        ///     The list of exceptions set by calling classes
        /// </summary>
        private List<Exception> exceptionList;

        /// <summary>
        ///     The list of other properties set by calling classes
        /// </summary>
        private List<object> otherPropertiesList;

        /// <summary>
        ///     The mocked application settings service
        /// </summary>
        private Mock<IAppSettings> appSettingsMock;

        /// <summary>
        ///     The test logger
        /// </summary>
        private ILogger testLogger;

        /// <summary>
        ///     The mocked Rest client
        /// </summary>
        private Mock<IRestClient> restClientMock;

        /// <summary>
        ///     The mocked Rest request
        /// </summary>
        private Mock<IRestRequest> restRequestMock;

        /// <summary>
        ///     The mocked wrapped Uri service
        /// </summary>
        private Mock<IUri> uriServiceMock;

        /// <summary>
        ///     The implementation to test
        /// </summary>
        private WebService helleHelloWorldWebService;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Instantiate lists
            this.logMessageList = new List<string>();
            this.exceptionList = new List<Exception>();
            this.otherPropertiesList = new List<object>();

            // Setup mocked dependencies
            this.appSettingsMock = new Mock<IAppSettings>();
            this.testLogger = new TestLogger(ref this.logMessageList, ref this.exceptionList, ref this.otherPropertiesList);
            this.restClientMock = new Mock<IRestClient>();
            this.restRequestMock = new Mock<IRestRequest>();
            this.uriServiceMock = new Mock<IUri>();

            // Create object to test
            this.helleHelloWorldWebService = new WebService(
                this.restClientMock.Object,
                this.restRequestMock.Object,
                this.appSettingsMock.Object,
                this.uriServiceMock.Object,
                this.testLogger);
        }

        /// <summary>
        ///     Test tear down. (runs after each test)
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            // Clear lists
            this.logMessageList.Clear();
            this.exceptionList.Clear();
            this.otherPropertiesList.Clear();
        }

        #region HelloWorldData
        /// <summary>
        ///     Tests the class's helloWorldData method for success when normal data was found
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataSuccess()
        {
            // Create return models for dependencies
            const string Data = "Hello World!!!";
            const string WebApiIUrl = "http://www.crowe-test.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<HellowWorldData>>();
            var helloData = GetHelloWorldData(Data);

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.ApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<HellowWorldData>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(helloData);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetHelloWorldContent();

            // Check values
            Assert.NotNull(response);
            Assert.AreEqual(response.Content, helloData.Content);
        }

        /// <summary>
        ///     Tests the class's HelloWorldData method for success when there is a null response
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullResponse()
        {
            // Create return models for dependencies
            const string Data = "Hello World!!!";
            const string WebApiIUrl = "http://www.crowe-test.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = (IRestResponse<HellowWorldData>)null;
            var helloWorldData = GetHelloWorldData(Data);
            const string ErrorMessage = "Did not get any response from the Hello World Web Api for the Method: GET /helloWorldContent";

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.ApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<HellowWorldData>(It.IsAny<IRestRequest>())).Returns(mockRestResponse);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetHelloWorldContent();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], ErrorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, ErrorMessage);
        }

        
    
        #endregion

        #region Helper Methods
        /// <summary>
        ///     Gets a sample HelloWorldData model
        /// </summary>
        /// <param name="data">HelloWorldData</param>
        /// <returns>HelloWorldData model</returns>
        private static HellowWorldData GetHelloWorldData(string data)
        {
            return new HellowWorldData { Content = data };
        }
        #endregion
    }
}