using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace Sedir.Server.Transportation
{
    public class SedirHttpServer : IRunnableSedirTransportationProtocol
    {
        private IHostBuilder HostBuilder { get; set; }

        public IRunnableSedirTransportationProtocol Build(int port)
        {
            HostBuilder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls($"http://localhost:{port}");
                });
            return this;
        }

        public void Run()
        {
            HostBuilder.Build().Start();
        }
    }
}