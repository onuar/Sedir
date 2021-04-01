namespace Sedir.Server.Transportation
{
    public class ServerResponse<TPayload>
        where TPayload : new()
    {
        public TPayload Payload { get; set; }
    }
}