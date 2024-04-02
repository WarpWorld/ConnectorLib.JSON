namespace ConnectorLib.JSON;

[Serializable]
public class MessageRequest : SimpleJSONRequest
{
    public string? message;
}