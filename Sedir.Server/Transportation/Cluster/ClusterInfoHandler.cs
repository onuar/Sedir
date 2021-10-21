using Microsoft.AspNetCore.Http;
using Sedir.Server.Transportation.Routing;

namespace Sedir.Server.Transportation.Cluster
{
    public class ClusterInfoHandler : IHandler
    {
        public object Execute(HttpRequest request)
        {
            return new NoContentResponsePayload();
        }

        public string GetPath()
        {
            return "/cluster/info";
        }
    }
}