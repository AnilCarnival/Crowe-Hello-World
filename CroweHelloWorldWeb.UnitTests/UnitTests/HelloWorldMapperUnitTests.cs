
namespace CroweHelloWorldWeb.Tests.UnitTests
{
    using CroweHelloWorldFoundation.Mappers;
    using CroweHelloWorldFoundation.Models;
    using NUnit.Framework;

    /// <summary>
    ///     Unit tests for the Hello World Mapper
    /// </summary>
    [TestFixture]
    public class HelloWorldMapperUnitTests
    {
        /// <summary>
        ///     The implementation to test
        /// </summary>
        private DataMapper helloWorldMapper;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Create object to test
            this.helloWorldMapper = new DataMapper();
        }

        #region HelloWorldData Tests
        /// <summary>
        ///     Tests the class's HelloWorldData method for success with a normal input value
        /// </summary>
        [Test]
        public void UnitTestHelloWorldMapperHelloWorldDataNormalSuccess()
        {
            const string Data = "Hello World!!!";

            // Create the expected result
            var expectedResult = GetHelloWorldData(Data);

            // Call the method to test
            var result = this.helloWorldMapper.GetHelloWorldContent(Data);

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Content, expectedResult.Content);
        }

        /// <summary>
        ///     Tests the HelloWorldData method for success with a null input value
        /// </summary>
        [Test]
        public void UnitTestHelloWorldMapperHelloWorldDataNullSuccess()
        {
            const string Data = null;

            // Create the expected result
            var expectedResult = GetHelloWorldData(Data);

            // Call the method to test
            var result = this.helloWorldMapper.GetHelloWorldContent(Data);

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Content, expectedResult.Content);
        }
        #endregion

        #region Helper Methods
        /// <summary>
        ///     Gets a HelloWorldData
        /// </summary>
        /// <param name="data">content </param>
        /// <returns>HelloWorldData model</returns>
        private static HellowWorldData GetHelloWorldData(string data)
        {
            return new HellowWorldData()
            {
                Content = data
            };
        }
        #endregion
    }
}