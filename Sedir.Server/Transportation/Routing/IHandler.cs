using Microsoft.AspNetCore.Http;

namespace Sedir.Server.Transportation.Routing
{
    public interface IHandler
    {
        object Execute(HttpRequest request);
        string GetPath();
    }
}