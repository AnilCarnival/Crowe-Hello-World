
namespace CroweHelloWorldFoundation.Services
{
    using CroweHelloWorldFoundation.Models;

    /// <summary>
    ///    service
    /// </summary>
    public interface IHelloWorldContentService
    {
        /// <summary>
        ///    Get HelloWorld Content from the file
        /// </summary>
        /// <returns>HellowWorldData model instance</returns>
        HellowWorldData GetHelloWorldContent();
    }
}