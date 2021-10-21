namespace Sedir.Server
{
    public class ServerConfiguration
    {
        public ServerConfiguration()
        {
            OtherNodeUrls = new string[] { };
        }

        public int NodeRunningPort { get; set; } = 5001;
        public string[] OtherNodeUrls { get; set; }
    }
}