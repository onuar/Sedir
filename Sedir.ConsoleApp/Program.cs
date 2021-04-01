using Sedir.Server;
using Sedir.Server.Transportation;

namespace Sedir.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var handler = new SedirHttpServer();
            var config = new ServerConfiguration();
            var server = new SedirServer(handler, config);
            
            server
                .Build()
                .Run();
            System.Console.WriteLine("Hello World!");
        }
    }
}