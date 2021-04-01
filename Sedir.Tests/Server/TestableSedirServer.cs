using Moq;
using Sedir.Server;
using Sedir.Server.Transportation;

namespace Sedir.Tests.Server
{
    public class TestableSedirServer : SedirServer
    {
        public TestableSedirServer(
            Mock<IRunnableSedirTransportationProtocol> sedirHandler,
            Mock<ServerConfiguration> configuration)
            : base(
                sedirHandler.Object,
                configuration.Object)
        {
            SedirHandler = sedirHandler;
            Configuration = configuration;
        }

        public Mock<IRunnableSedirTransportationProtocol> SedirHandler { get; }

        public Mock<ServerConfiguration> Configuration { get; set; }

        public static TestableSedirServer Create()
        {
            return new TestableSedirServer(
                new Mock<IRunnableSedirTransportationProtocol>(MockBehavior.Loose),
                new Mock<ServerConfiguration>(MockBehavior.Loose));
        }
    }
}