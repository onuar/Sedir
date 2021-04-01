using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace Sedir.Server.Transportation
{
    public class SedirHttpServer : IRunnableSedirTransportationProtocol
    {
        private IHostBuilder HostBuilder { get; set; }

        public IRunnableSedirTransportationProtocol Build()
        {
            HostBuilder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
            return this;
        }

        public void Run()
        {
            HostBuilder.Build().Start();
        }
    }
}