using System;

namespace Sedir.Server
{
    public interface ISedirServer : IDisposable
    {
        IRunnableSedirServer Build();
        bool IsRunning { get; set; }
        NodeRole Role { get; set; }
    }
}