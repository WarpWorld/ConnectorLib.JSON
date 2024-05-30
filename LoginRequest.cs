﻿#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
namespace ConnectorLib.JSON;

#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
public class LoginRequest : SimpleJSONRequest
{
    public string? login;
    public string? password;
}