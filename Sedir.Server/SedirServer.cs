using Sedir.Server.Transportation;

namespace Sedir.Server
{
    public class SedirServer : IRunnableSedirServer
    {
        private readonly IRunnableSedirTransportationProtocol _sedirHandler;
        private readonly ServerConfiguration _configuration;

        public SedirServer(IRunnableSedirTransportationProtocol sedirHandler,
            ServerConfiguration configuration = null)
        {
            _sedirHandler = sedirHandler;
            _configuration = configuration;

            Role = NodeRole.Leader;

            if (configuration != null)
            {
                if (configuration.Urls.Length > 0)
                {
                    Role = NodeRole.Node;
                }
            }
        }

        public void Dispose()
        {
            IsRunning = false;
        }

        public IRunnableSedirServer Build()
        {
            _sedirHandler.Build();
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