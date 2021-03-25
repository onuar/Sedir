using Moq;

namespace Sedir.Tests.Server
{
    public class TestableSedirServer : SedirServer
    {
        public ServerConfiguration ServerConfiguration { get; set; } = new Mock<ServerConfiguration>().Object;
        public TransportationProtocol TransportationProtocol { get; set; } = new Mock<TransportationProtocol>().Object;

        public TestableSedirServer()
            : base(new Mock<TransportationProtocol>().Object, new Mock<ServerConfiguration>().Object)
        {
        }

        public SedirServer Create()
        {
            return new SedirServer(TransportationProtocol, ServerConfiguration);
        }
    }
}