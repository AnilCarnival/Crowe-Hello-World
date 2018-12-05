
namespace CroweHelloWorldWeb.Tests.UnitTests
{
    using System;
    using System.Configuration;
    using System.IO;
    using CroweHelloWorldFoundation.FrameworkWrappers;
    using CroweHelloWorldFoundation.Mappers;
    using CroweHelloWorldFoundation.Models;
    using CroweHelloWorldFoundation.Resources;
    using CroweHelloWorldFoundation.Services;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    ///     Unit tests for the Hello World Data Service
    /// </summary>
    [TestFixture]
    public class HelloWorldDataServiceUnitTests
    {

        /// <summary>
        ///     The mocked Hello World Mapper
        /// </summary>
        private Mock<IDataMapper> helloWorldMapperMock;

        /// <summary>
        ///     The implementation to test
        /// </summary>
        private HelloWorldContentService helloWorldContentService;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            this.helloWorldMapperMock = new Mock<IDataMapper>();

            // Create object to test
            this.helloWorldContentService = new HelloWorldContentService(
                this.helloWorldMapperMock.Object);
        }

        
        /// <summary>
        ///     Tests the class's HelloWorldData method for success
        /// </summary>
        [Test]
        public void UnitTestHelloWorldDataServiceGetHelloWorldContentSuccess()
        {
            // Create return models for dependencies
            const string content = "Hello World!!!";
            var rawData = content;

            // Create the expected result
            var expectedResult = GetTestHelloWorldContent(rawData);

            // Set up dependencies
            this.helloWorldMapperMock.Setup(m => m.GetHelloWorldContent(rawData)).Returns(expectedResult);

            // Call the method to test
            var result = this.helloWorldContentService.GetHelloWorldContent();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Content, expectedResult.Content);
        }


        
        /// <summary>
        ///     Gets HellowWorldData
        /// </summary>
        /// <param name="data">content</param>
        /// <returns>HellowWorldData</returns>
        private static HellowWorldData GetTestHelloWorldContent(string content)
        {
            return new HellowWorldData { Content = content };
        }
        
    }
}