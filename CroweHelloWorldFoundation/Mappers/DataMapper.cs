
namespace CroweHelloWorldFoundation.Mappers
{
    using CroweHelloWorldFoundation.Models;

    /// <summary>
    ///     Mapper service for mapping types for the Hello World data service
    /// </summary>
    public class DataMapper : IDataMapper
    {
        /// <summary>
        ///     Maps a string to a HelloWorldData
        /// </summary>
        /// <param name="input">The input</param>
        /// <returns>HelloWorldData model</returns>
        public HellowWorldData GetHelloWorldContent(string data)
        {
            return new HellowWorldData { Content = data };
        }
    }
}