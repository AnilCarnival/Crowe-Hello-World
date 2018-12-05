
namespace CroweHelloWorldWeb.Tests.UnitTests
{
    using System.Configuration;
    using System.IO;
    using Controllers;
    using CroweHelloWorldFoundation.Models;
    using CroweHelloWorldFoundation.Services;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    ///     Unit tests for the GetHellowWorldData Controller
    /// </summary>
    [TestFixture]
    public class HelloWorldControllerUnitTests
    {
        /// <summary>
        ///     The mocked data service
        /// </summary>
        private Mock<IHelloWorldContentService> dataServiceMock;

        /// <summary>
        ///     The implementation to test
        /// </summary>
        private HelloWorldController helloWorldDataController;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            this.dataServiceMock = new Mock<IHelloWorldContentService>();

            // Create object to test
            this.helloWorldDataController = new HelloWorldController(this.dataServiceMock.Object);
        }

        #region Get Tests
        /// <summary>
        ///     Tests the controller's get method for success
        /// </summary>
        [Test]
        public void UnitTesthelloWorldDataControllerGetSuccess()
        {
            // Create the expected result
            var expectedResult = GetHellowWorldData();

            // Set up dependencies
            this.dataServiceMock.Setup(m => m.GetHelloWorldContent()).Returns(expectedResult);

            // Call the method to test
            var result = this.helloWorldDataController.Get();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Content, expectedResult.Content);
        }

      
        #endregion

        #region Helper Methods
        /// <summary>
        ///     Gets a sample HellowWorld model
        /// </summary>
        /// <returns>A sample HellowWorld model</returns>
        private static HellowWorldData GetHellowWorldData()
        {
            return new HellowWorldData()
            {
                Content = "Hello World!!!"
            };
        }
        #endregion
    }
}