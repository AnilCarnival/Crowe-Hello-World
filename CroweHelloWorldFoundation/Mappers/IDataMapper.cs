
namespace CroweHelloWorldFoundation.Mappers
{
    using CroweHelloWorldFoundation.Models;

    /// <summary>
    ///     Mapper service for mapping types for the Hello World data service
    /// </summary>
    public interface IDataMapper
    {
        /// <summary>
        ///     HellowWorldData model mapping
        /// </summary>
        /// <param name="data">data</param>
        /// <returns>HellowWorldData instance</returns>
        HellowWorldData GetHelloWorldContent(string data);
    }
}