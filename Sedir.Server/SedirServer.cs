using System;
using System.Collections.Generic;
using System.Linq;
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
            NotifyOtherNodes();
            IsRunning = true;
        }

        private void NotifyOtherNodes()
        {
            if (Role != NodeRole.Leader)
            {
                ClusterInfoManager.AddNodes(_configuration.OtherNodeUrls);
            }
        }

        public bool IsRunning { get; set; }
        public NodeRole Role { get; set; }
    }

    internal class ClusterInfoManager
    {
        public static void AddNodes(string[] nodeUrls)
        {
            ExtractBaseUrlsAndPorts(nodeUrls);
        }

        private static List<NodeInfo> ExtractBaseUrlsAndPorts(string[] nodeUrls)
        {
            var nodeInfos = nodeUrls.Select(_ => new NodeInfo()
                {BaseUrl = _.Split(":")[0], Port = Convert.ToInt32(_.Split(":")[1])}).ToList();
            return nodeInfos;
        }
    }

    internal class NodeInfo
    {
        public string BaseUrl { get; set; }
        public int Port { get; set; }
    }
}