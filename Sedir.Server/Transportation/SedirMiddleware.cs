using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Sedir.Server.Transportation.Routing;

namespace Sedir.Server.Transportation
{
    public class SedirMiddleware : IMiddleware
    {
        private readonly IServiceProvider _serviceProvider;

        public SedirMiddleware(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            GuardForUnnecessaryPaths(context);
            var routingList = _serviceProvider.GetServices<IHandler>().ToList();
            var routing = routingList.SingleOrDefault(x => x.GetPath() == context.Request.Path);

            if (routing == null)
            {
                return WriteResponse(context, "404",
                    new NoHandlerFoundResponse() {Message = $"No routing found for {context.Request.Path}"});
            }

            var responsePayload = routing.Execute(context.Request);
            return WriteResponse(context, "200", responsePayload);
        }

        private Task GuardForUnnecessaryPaths(HttpContext context)
        {
            if (context.Request.Path.Equals("/favicon.ico"))
            {
                return WriteResponse(context, "404",
                    new NoHandlerFoundResponse() {Message = $"No routing found for {context.Request.Path}"});
            }

            return Task.CompletedTask;
        }

        private Task WriteResponse(HttpContext context, string status, object payload)
        {
            return context.Response.WriteAsJsonAsync(new SedirResponse()
            {
                Status = status,
                Payload = payload
            });
        }
    }

    public class SedirResponse
    {
        public object Payload { get; set; }
        public string Status { get; set; }
    }
}