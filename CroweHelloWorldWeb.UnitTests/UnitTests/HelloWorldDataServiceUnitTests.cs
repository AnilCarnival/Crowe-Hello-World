
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
        ///     The mocked application settings service
        /// </summary>
        private Mock<IAppSettings> appSettingsMock;

        /// <summary>
        ///     The mocked File IO service
        /// </summary>
        private Mock<IFileIOService> fileIOServiceMock;

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
            this.appSettingsMock = new Mock<IAppSettings>();
            this.fileIOServiceMock = new Mock<IFileIOService>();
            this.helloWorldMapperMock = new Mock<IDataMapper>();

            // Create object to test
            this.helloWorldContentService = new HelloWorldContentService(
                this.appSettingsMock.Object,
                this.fileIOServiceMock.Object,
                this.helloWorldMapperMock.Object);
        }

        #region Get HelloWorldDataTests
        /// <summary>
        ///     Tests the class's HelloWorldData method for success
        /// </summary>
        [Test]
        public void UnitTestHelloWorldDataServiceGetHelloWorldContentSuccess()
        {
            // Create return models for dependencies
            const string DataFilePath = "test/path";
            const string FileContents = "Hello World text";
            var rawData = FileContents;

            // Create the expected result
            var expectedResult = GetTestHelloWorldContent(rawData);

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HellowWorldFileName)).Returns(DataFilePath);
            this.fileIOServiceMock.Setup(m => m.ReadFile(DataFilePath)).Returns(FileContents);
            this.helloWorldMapperMock.Setup(m => m.GetHelloWorldContent(rawData)).Returns(expectedResult);

            // Call the method to test
            var result = this.helloWorldContentService.GetHelloWorldContent();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Content, expectedResult.Content);
        }

        /// <summary>
        ///     Tests the class's HelloWorldData method when the setting key is null
        /// </summary>
        [Test]
        [ExpectedException(ExpectedException = typeof(SettingsPropertyNotFoundException), ExpectedMessage = ErrorCodes.HelloWorldFileSettingsKeyError)]
        public void UnitTestHelloWorldDataServiceGetHelloWorldContentSettingKeyNull()
        {
            // Create return models for dependencies
            const string DataFilePath = null;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HellowWorldFileName)).Returns(DataFilePath);

            // Call the method to test
            this.helloWorldContentService.GetHelloWorldContent();
        }

        /// <summary>
        ///     Tests the class's HelloWorldData method when the setting key is an empty string
        /// </summary>
        [Test]
        [ExpectedException(ExpectedException = typeof(SettingsPropertyNotFoundException), ExpectedMessage = ErrorCodes.HelloWorldFileSettingsKeyError)]
        public void UnitTestHelloWorldDataServiceGetHelloWorldContentSettingKeyEmptyString()
        {
            // Create return models for dependencies
            var dataFilePath = string.Empty;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HellowWorldFileName)).Returns(dataFilePath);

            // Call the method to test
            this.helloWorldContentService.GetHelloWorldContent();
        }

        /// <summary>
        ///     Tests the class's HelloWorldData method for an IO Exception
        /// </summary>
        [Test]
        [ExpectedException(ExpectedException = typeof(IOException))]
        public void UnitTestHelloWorldDataServiceGetHelloWorldContentIOException()
        {
            // Create return models for dependencies
            const string DataFilePath = "test/path";

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HellowWorldFileName)).Returns(DataFilePath);
            this.fileIOServiceMock.Setup(m => m.ReadFile(DataFilePath)).Throws(new IOException("Error!"));

            // Call the method to test
            this.helloWorldContentService.GetHelloWorldContent();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        ///     Gets HellowWorldData
        /// </summary>
        /// <param name="data">content</param>
        /// <returns>HellowWorldData</returns>
        private static HellowWorldData GetTestHelloWorldContent(string content)
        {
            return new HellowWorldData { Content = content };
        }
        #endregion
    }
}