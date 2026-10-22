#if !PROTOCOL_JSON
using CrowdControl.Common.Converters;
#else
using System.Diagnostics.CodeAnalysis;
#endif
using Newtonsoft.Json;

#if PROTOCOL_JSON
namespace ConnectorLib.JSON
{
    public partial class EffectUpdate
    {
        /// <summary>The type of object this report pertains to.</summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [JsonConverter(typeof(CamelCaseStringEnumConverter))]
        public enum IdentifierType
        {
            Effect = 0,
            Group = 1,
            Category = 2
        }
    }
}
#else
namespace CrowdControl.Common
{
    /// <summary>The type of object identified by an effect update.</summary>
    [JsonConverter(typeof(CamelCaseStringEnumConverter<IdentifierType>))]
    public enum IdentifierType
    {
        Effect = 0,
        Group = 1,
        Category = 2
    }
}
#endif