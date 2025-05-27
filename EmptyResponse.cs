#if NETSTANDARD1_3_OR_GREATER
using System;
#endif
namespace ConnectorLib.JSON;

/// <summary>An empty response.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
public class EmptyResponse : SimpleJSONResponse;
