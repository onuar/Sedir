using NUnit.Framework;
using Sedir.Server;

namespace Sedir.Tests.Server
{
    [TestFixture]
    public class SedirServerTests
    {
        [Test]
        public void SedirServerShouldBeCreatedAsSingleNode()
        {
            ServerConfiguration configuration = new ServerConfiguration()
            {
                Port = 6665
            };
            using TestableSedirServer server = new TestableSedirServer();
            server.ServerConfiguration = configuration;
            server.Run();
            Assert.IsTrue(server.IsRunning);
        }

        [Test]
        public void SedirServerShouldBeStoppedAfterDisposing()
        {
            ISedirServer server;
            using (server = new TestableSedirServer())
            {
                server.Run();
            }

            Assert.IsFalse(server.IsRunning);
        }

        [Test]
        public void NewSedirServerShouldBeAddedToClusterAsANodeWhenExistingNodeUrlIsGiven()
        {
            var config = new ServerConfiguration()
            {
                Urls = new[]
                {
                    "127.0.0.1:6665"
                }
            };
            ISedirServer server = new TestableSedirServer() {ServerConfiguration = config}.Create();
            Assert.AreEqual(server.Role, NodeRole.Node);
        }

        [Test]
        public void SedirServerShouldBeLeaderIfConfigurationIsNotGiven()
        {
            ISedirServer server = new TestableSedirServer();
            Assert.AreEqual(server.Role, NodeRole.Leader);
        }

        [Test]
        public void SedirServerShouldHaveATransportationProtocol()
        {
            TestableSedirServer sedirServer = new TestableSedirServer();
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