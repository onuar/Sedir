using System;

namespace Sedir.Server
{
    public interface ISedirServer : IDisposable
    {
        void Run();
        bool IsRunning { get; set; }
        NodeRole Role { get; set; }
    }
}