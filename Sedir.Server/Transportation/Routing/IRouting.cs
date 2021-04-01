using Microsoft.AspNetCore.Http;

namespace Sedir.Server.Transportation.Routing
{
    public interface IRouting
    {
        object Execute(HttpRequest request);
        string GetPath();
    }
}