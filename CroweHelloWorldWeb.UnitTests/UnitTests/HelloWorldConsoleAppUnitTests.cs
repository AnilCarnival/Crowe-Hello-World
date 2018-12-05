
namespace CroweHelloWorldWeb.Tests.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Crowe.ConsoleApplication;
    using CroweHelloWorldFoundation.Models;
    using CroweHelloWorldFoundation.Services;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    ///     Unit tests for the Hello World Console App
    /// </summary>
    [TestFixture]
    public class HelloWorldConsoleAppUnitTests
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
        ///     The mocked Hello World Web Service
        /// </summary>
        private Mock<IWebService> helloWorldWebServiceMock;

        /// <summary>
        ///     The test logger
        /// </summary>
        private ILogger testLogger;

        /// <summary>
        ///     The implementation to test
        /// </summary>
        private CroweHelloWorldConsole helloWorldConsoleApp;

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
            this.helloWorldWebServiceMock = new Mock<IWebService>();
            this.testLogger = new TestLogger(ref this.logMessageList, ref this.exceptionList, ref this.otherPropertiesList);

            // Create object to test
            this.helloWorldConsoleApp = new CroweHelloWorldConsole(this.helloWorldWebServiceMock.Object, this.testLogger);
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

        #region Run Tests
        /// <summary>
        ///     Tests the class's Run method for success when normal data was found
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataSuccess()
        {
            const string Data = "Hello World!!!";

            // Create return models for dependencies
            var helloWorldData = GetHelloWorldData(Data);

            // Set up dependencies
            this.helloWorldWebServiceMock.Setup(m => m.GetHelloWorldContent()).Returns(helloWorldData);

            // Call the method to test
            this.helloWorldConsoleApp.Run(null);

            // Check values
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], Data);
        }

        /// <summary>
        ///     Tests the class's Run method for success when null data was found
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNullDataSuccess()
        {
            const string Data = null;
            HellowWorldData helloWorldData = GetHelloWorldData(Data);
            // Set up dependencies
            this.helloWorldWebServiceMock.Setup(m => m.GetHelloWorldContent()).Returns(helloWorldData);

            // Call the method to test
            this.helloWorldConsoleApp.Run(null);

            // Check values
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], "No data was found!");
        }
        #endregion

        #region Helper Methods
        /// <summary>
        ///    HelloWorld data model
        /// </summary>
        /// <param name="data">content</param>
        /// <returns>HelloWorld datamodel</returns>
        private static HellowWorldData GetHelloWorldData(string data)
        {
            return new HellowWorldData { Content = data };
        }
        #endregion
    }
}