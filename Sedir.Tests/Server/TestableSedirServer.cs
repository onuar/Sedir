using Moq;
using Sedir.Server;
using Sedir.Server.Transportation;

namespace Sedir.Tests.Server
{
    public class TestableSedirServer : SedirServer
    {
        public ServerConfiguration ServerConfiguration { get; set; } = new Mock<ServerConfiguration>().Object;
        public SedirHttpServer SedirHttpServer { get; set; } = new Mock<SedirHttpServer>().Object;

        public TestableSedirServer()
            : base(new Mock<SedirHttpServer>().Object, new Mock<ServerConfiguration>().Object)
        {
        }

        public SedirServer Create()
        {
            return new SedirServer(SedirHttpServer, ServerConfiguration);
        }
    }
}