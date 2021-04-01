using Sedir.Server.Transportation.Routing;

namespace Sedir.Server.Transportation.HealthCheck
{
    public class HealthCheckResponsePayload : IResponsePayload
    {
        public string Status { get; set; }
    }
}