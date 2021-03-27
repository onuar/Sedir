namespace Sedir.Server
{
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