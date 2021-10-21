namespace Sedir.Server.Transportation
{
    public interface ISedirTransportationProtocol
    {
        IRunnableSedirTransportationProtocol Build(int port);
    }
}