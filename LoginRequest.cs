namespace ConnectorLib.JSON;

[Serializable]
public class LoginRequest : SimpleJSONRequest
{
    public string? login;
    public string? password;
}