#if NETSTANDARD1_3_OR_GREATER
using System;
#endif

namespace ConnectorLib.JSON;

/// <summary>An empty request.</summary>
#if NETSTANDARD1_3_OR_GREATER
[Serializable]
#endif
public class EmptyRequest : SimpleJSONRequest;
