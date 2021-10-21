using System;
using Sedir.Server.Transportation;

namespace Sedir.Server
{
    public class SedirServer : IRunnableSedirServer
    {
        private readonly IRunnableSedirTransportationProtocol _sedirHandler;
        private readonly ServerConfiguration _configuration;

        public SedirServer(
            IRunnableSedirTransportationProtocol sedirHandler,
            ServerConfiguration configuration)
        {
            _sedirHandler = sedirHandler ?? throw new ArgumentNullException("sedirHandler");
            _configuration = configuration ?? throw new ArgumentNullException("configuration");

            Role = NodeRole.Leader;
            if (configuration.OtherNodeUrls.Length > 0)
            {
                Role = NodeRole.Node;
            }
        }

        public void Dispose()
        {
            IsRunning = false;
        }

        public IRunnableSedirServer Build()
        {
            _sedirHandler.Build(_configuration.NodeRunningPort);
            return this;
        }

        public void Run()
        {
            _sedirHandler.Run();
            IsRunning = true;
        }

        public bool IsRunning { get; set; }
        public NodeRole Role { get; set; }
    }
}