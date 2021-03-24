using System;
using NUnit.Framework;

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
            using ISedirServer server = new SedirServer(configuration);
            server.Run();
            Assert.IsTrue(server.IsRunning);
        }

        [Test]
        public void SedirServerShouldBeStoppedAfterDisposing()
        {
            ISedirServer server;
            using (server = new SedirServer())
            {
                server.Run();
            }

            Assert.IsFalse(server.IsRunning);
        }

        [Test]
        public void NewSedirServerShouldBeAddedToClusterAsANodeWhenExistingNodeUrlIsGiven()
        {
            ISedirServer server = new SedirServer(new ServerConfiguration()
            {
                Urls = new[]
                {
                    "127.0.0.1:6665"
                }
            });
            Assert.AreEqual(server.Role, ServerRole.Node);
        }

        [Test]
        public void SedirServerShouldBeLeaderIfConfigurationIsNotGiven()
        {
            ISedirServer server = new SedirServer();
            Assert.AreEqual(server.Role, ServerRole.Leader);
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

    public enum ServerRole
    {
        Node,
        Leader
    }

    public interface ISedirServer : IDisposable
    {
        void Run();
        bool IsRunning { get; set; }
        ServerRole Role { get; set; }
    }

    public class SedirServer : ISedirServer
    {
        public SedirServer(ServerConfiguration configuration = null)
        {
            Role = ServerRole.Leader;

            if (configuration != null)
            {
                if (configuration.Urls.Length > 0)
                {
                    Role = ServerRole.Node;
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

        public bool IsRunning { get; set; }
        public ServerRole Role { get; set; }
    }

    public interface ISedirHandler<TRequest, TResponse>
        where TResponse : HandlerResponse
    {
        public TResponse Accept(TRequest request);
    }

    public abstract class HandlerResponse
    {
    }

    public class ServerConfiguration
    {
        public ServerConfiguration()
        {
            Urls = new string[] { };
        }

        public int Port { get; set; }
        public string[] Urls { get; set; }
    }
}