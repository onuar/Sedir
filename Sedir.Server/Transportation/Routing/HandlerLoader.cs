using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Sedir.Server.Transportation.Routing
{
    public static class HandlerLoader
    {
        public static void AddSedirRouting(this IServiceCollection serviceCollection)
        {
            var routingType = typeof(IHandler);
            var assembly = Assembly.Load(typeof(IHandler).Assembly.FullName);

            foreach (var implementationType in assembly.GetTypes()
                .Where(type => routingType.IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract))
            {
                serviceCollection.AddSingleton(routingType, implementationType);
            }
        }
    }
}