using Moq;
using NUnit.Framework;
using Sedir.Server;
using Sedir.Server.Transportation;

namespace Sedir.Tests.Server
{
    [TestFixture]
    public class SedirServerTests
    {
        [Test]
        public void SedirServerShouldBeCreatedAsSingleNode()
        {
            using TestableSedirServer server = TestableSedirServer.Create();
            server.Configuration.Object.Port = 6665;
            server.Run();
            Assert.IsTrue(server.IsRunning);
        }

        [Test]
        public void SedirServerShouldBeStoppedAfterDisposing()
        {
            IRunnableSedirServer server;
            using (server = TestableSedirServer.Create())
            {
                server.Run();
            }

            Assert.IsFalse(server.IsRunning);
        }

        [Test]
        public void NewSedirServerShouldBeAddedToClusterAsANodeWhenExistingNodeUrlIsGiven()
        {
            var handler = new Mock<IRunnableSedirTransportationProtocol>();
            Mock<ServerConfiguration> config = new Mock<ServerConfiguration>();
            config.Object.Urls = new[]
                {"127.0.0.1:6665"};

            TestableSedirServer server = new TestableSedirServer(handler, config);

            server.Configuration = config;
            Assert.AreEqual(server.Role, NodeRole.Node);
        }

        [Test]
        public void SedirServerShouldBeLeaderIfConfigurationIsNotGiven()
        {
            TestableSedirServer server = TestableSedirServer.Create();
            Assert.AreEqual(server.Role, NodeRole.Leader);
        }


        [Test]
        public void SedirServerBuildShouldCallTransportationBuild()
        {
            Mock<IRunnableSedirTransportationProtocol> handler = new Mock<IRunnableSedirTransportationProtocol>();
            Mock<ServerConfiguration> config = new Mock<ServerConfiguration>();

            handler.Setup(x => x.Build());
            TestableSedirServer sedirServer = new TestableSedirServer(handler, config);
            sedirServer.Build();
            handler.Verify(x => x.Build(), Times.Once);
        }

        [Test]
        public void SedirServerRunShouldCallTransportationRun()
        {
            Mock<IRunnableSedirTransportationProtocol> handler = new Mock<IRunnableSedirTransportationProtocol>();
            Mock<ServerConfiguration> config = new Mock<ServerConfiguration>();

            handler.Setup(x => x.Run());
            TestableSedirServer sedirServer = new TestableSedirServer(handler, config);
            sedirServer.Run();
            handler.Verify(x => x.Run(), Times.Once);
        }

        // [Test]
        // public void SedirServerShouldExposeTopologyHandlerWhenRunning()
        // {
        //     ServerConfiguration configuration = new ServerConfiguration();
        //
        //     using (SedirServer server = new SedirServer(configuration, topologyHandler))
        //     {
        //         server.Run();
        //     }
        // }

        //leader 127.0.0.1:6665/
        //node 127.0.0.1:6666/ >>> POST 127.0.0.1:6665/topology/newnode
        //node 127.0.0.1:6667/ >>> POST 127.0.0.1:6665/topology/newnode
        //node 127.0.0.1:6668/ >>> POST 127.0.0.1:6667/topology/newnode

        //127.0.0.1:6666 >>> GET x.x.x.x:6665/heartbeat
        //127.0.0.1:6666/STARTELECTION

        //PUT 127.0.0.1:6665/database/
    }
}