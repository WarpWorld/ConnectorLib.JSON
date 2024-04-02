using System.Diagnostics.CodeAnalysis;

namespace ConnectorLib.JSON;

[Serializable]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class EffectResponse : SimpleJSONResponse
{
    public EffectStatus status;
    public string? message;
    /// <remarks>
    /// If applicable (messages 0x00 (sometimes) and 0x07 (always)), this should contain the time remaining on the running effect, in milliseconds.
    /// Otherwise, this field should be 0 or missing.
    /// </remarks>
    public long timeRemaining; //milliseconds
}
