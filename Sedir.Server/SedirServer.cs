namespace Sedir.Server
{
    public class SedirServer : IRunnableSedirServer
    {
        private readonly TransportationProtocol _sedirHttpHandler;
        private readonly ServerConfiguration _configuration;

        public SedirServer(TransportationProtocol sedirHttpHandler, ServerConfiguration configuration = null)
        {
            _sedirHttpHandler = sedirHttpHandler;
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

        public void Run()
        {
            IsRunning = true;
        }

        public IRunnableSedirServer Build()
        {
            return this;
        }

        public bool IsRunning { get; set; }
        public NodeRole Role { get; set; }
    }
}