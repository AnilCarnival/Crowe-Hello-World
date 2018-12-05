
namespace Crowe.ConsoleApplication
{
    using CroweHelloWorldFoundation.Models;

    /// <summary>
    ///     Service Hello World  API
    /// </summary>
    public interface IWebService
    {
        /// <summary>
        ///    GetHelloWorldContent
        /// </summary>
        /// <returns>HellowWorldData model</returns>
        HellowWorldData GetHelloWorldContent();
    }
}