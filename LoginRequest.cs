#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
namespace ConnectorLib.JSON;

/// <summary>Request to log in.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
public class LoginRequest : SimpleJSONRequest
{
    /// <summary>The login name of the user.</summary>
    public string? login;
    
    /// <summary>The password of the user.</summary>
    public string? password;
}