namespace Sedir.Server.Transportation.Routing
{
    public class NoContentResponsePayload : IResponsePayload
    {
        public string Message { get; } = "No content";
    }
}