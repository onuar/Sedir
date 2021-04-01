using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Sedir.Server.Transportation
{
    public class SedirMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //dispatch to routing and get payload
            // return context.Response.WriteAsJsonAsync(new ServerResponse<>());
            return context.Response.WriteAsync("ok");
        }
    }
}