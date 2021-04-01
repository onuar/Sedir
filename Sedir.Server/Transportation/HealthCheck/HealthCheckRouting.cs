using Microsoft.AspNetCore.Http;
using Sedir.Server.Transportation.Routing;

namespace Sedir.Server.Transportation.HealthCheck
{
    public class HealthCheckRouting : IRouting
    {
        public object Execute(HttpRequest request)
        {
            return new HealthCheckResponsePayload() {Status = "OK Computer!"};
        }

        public string GetPath()
        {
            return "/hc";
        }
    }
}