using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

/// <summary>Source details for an effect.</summary>
public interface IEffectSourceDetails
{
    /// <summary>The type of effect source.</summary>
    [JsonProperty("type")]
    public string Type { get; }

    /// <summary>Converter for serializing and deserializing <see cref="IEffectSourceDetails"/>.</summary>
    public class Converter : JsonConverter<IEffectSourceDetails?>
    {
        /// <summary>Singleton instance of the converter.</summary>
        public static readonly Converter Instance = new();

        public override IEffectSourceDetails? ReadJson(JsonReader reader, Type objectType, IEffectSourceDetails? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            JObject jObject = JObject.Load(reader);
            string? type = jObject["type"]?.Value<string>();

            return type switch
            {
                "twitch-channel-reward" => jObject.ToObject<TwitchChannelRewardSourceDetails>(),
                "stream-labs-donation" => jObject.ToObject<StreamLabsDonationSourceDetails>(),
                "event-hype-train" => jObject.ToObject<HypeTrainSourceDetails>(),
                "tiktok-gift" => jObject.ToObject<TikTokGiftSourceDetails>(),
                "tiktok-like" => jObject.ToObject<TikTokLikeSourceDetails>(),
                "tiktok-follow" => jObject.ToObject<TikTokFollowSourceDetails>(),
                "tiktok-share" => jObject.ToObject<TikTokShareSourceDetails>(),
                "pulsoid-trigger" => jObject.ToObject<PulsoidTriggerSourceDetails>(),
                "crowd-control-test" => jObject.ToObject<CrowdControlTestSourceDetails>(),
                "crowd-control-chaos-mode" => jObject.ToObject<CrowdControlChaosModeSourceDetails>(),
                "crowd-control-retry" => jObject.ToObject<CrowdControlRetrySourceDetails>(),
                _ => null
            };
        }

        public override void WriteJson(JsonWriter writer, IEffectSourceDetails? value, JsonSerializer serializer)
            => serializer.Serialize(writer, (value == null) ? null : JObject.FromObject(value));
    }
}

/// <summary>Details of a Twitch channel reward.</summary>
public class TwitchChannelRewardSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "twitch-channel-reward";

    /// <summary>Twitch channel reward ID.</summary>
    [JsonProperty("rewardID")]
    public string RewardID { get; set; }

    /// <summary>Twitch channel reward redemption ID.</summary>
    [JsonProperty("redemptionID")]
    public string RedemptionID { get; set; }

    /// <summary>Twitch user ID of the reward redeemer.</summary>
    [JsonProperty("twitchID")]
    public string TwitchID { get; set; }

    /// <summary>Twitch display name of the reward redeemer.</summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>The cost of the reward in channel points.</summary>
    [JsonProperty("cost")]
    public int Cost { get; set; }
}

public class StreamLabsDonationSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "stream-labs-donation";

    /// <summary>StreamLabs donation ID.</summary>
    [JsonProperty("donationID")]
    public string DonationID { get; set; }
    
    /// <summary>The amount of the donation.</summary>
    [JsonProperty("cost")]
    public string Cost { get; set; }

    /// <summary>The currency of the donation.</summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>Twitch display name of the donor.</summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>The message of the donation.</summary>
    [JsonProperty("message")]
    public string? Message { get; set; }
}

/// <summary>Details of a Hype Train event.</summary>
public class HypeTrainSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "event-hype-train";

    /// <summary>The total number of bits contributed to the Hype Train.</summary>
    [JsonProperty("total")]
    public int Total { get; set; }

    /// <summary>The current progress of the Hype Train.</summary>
    [JsonProperty("progress")]
    public int Progress { get; set; }

    /// <summary>The goal to reach for the next level of the Hype Train.</summary>
    [JsonProperty("goal")]
    public int Goal { get; set; }

    /// <summary>The number of viewers who have contributed to the Hype Train.</summary>
    [JsonProperty("top_contributions")]
    public List<Contribution> TopContributions { get; set; }

    /// <summary>The last contribution to the Hype Train.</summary>
    [JsonProperty("last_contribution")]
    public Contribution LastContribution { get; set; }

    /// <summary>The level of the Hype Train.</summary>
    [JsonProperty("level")]
    public int Level { get; set; }

    /// <summary>Represents a contribution to the Hype Train.</summary>
    public class Contribution
    {
        /// <summary>Twitch user ID of the contributor.</summary>
        [JsonProperty("user_id")]
        public string UserID { get; set; }

        /// <summary>Twitch login name of the contributor.</summary>
        [JsonProperty("user_login")]
        public string UserLogin { get; set; }

        /// <summary>Twitch display name of the contributor.</summary>
        [JsonProperty("user_name")]
        public string UserName { get; set; }

        /// <summary>The type of contribution (<c>"bits"</c> or <c>"subscription"</c>).</summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>The amount of the contribution.</summary>
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}

/// <summary>Details of a TikTok event.</summary>
public abstract class TikTokSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public abstract string Type { get; }

    /// <summary>The cost of the gift in TikTok coins.</summary>
    /// <remarks>This field is only initialized in <see cref="TikTokGiftSourceDetails"/> objects. It will be zero in other messages.</remarks>
    [JsonProperty("cost")]
    public int Cost { get; set; }
    
    /// <summary>The TikTok display name of the gift sender.</summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>The TikTok user ID of the gift sender.</summary>
    [JsonProperty("userID")]
    public string UserID { get; set; }
}

/// <summary>Details of a TikTok gift event.</summary>
public class TikTokGiftSourceDetails : TikTokSourceDetails
{
    [JsonProperty("type")]
    public override string Type =>"tiktok-gift";
    
    /// <summary>The ID of the gift.</summary>
    [JsonProperty("giftID")]
    public int GiftID { get; set; }

    /// <summary>The name of the gift.</summary>
    [JsonProperty("giftName")]
    public string GiftName { get; set; }

    /// <summary>The transaction ID of the gift.</summary>
    [JsonProperty("transactionID")]
    public string? TransactionID { get; set; }
}

/// <summary>Details of a TikTok like event.</summary>
public class TikTokLikeSourceDetails : TikTokSourceDetails
{
    [JsonProperty("type")]
    public override string Type => "tiktok-like";
}

/// <summary>Details of a TikTok follow event.</summary>
public class TikTokFollowSourceDetails : TikTokSourceDetails
{
    [JsonProperty("type")]
    public override string Type => "tiktok-follow";
}

/// <summary>Details of a TikTok share event.</summary>
public class TikTokShareSourceDetails : TikTokSourceDetails
{
    [JsonProperty("type")]
    public override string Type => "tiktok-share";
}

/// <summary>Details of a Pulsoid trigger event.</summary>
public class PulsoidTriggerSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "pulsoid-trigger";

    /// <summary>The heart rate of the user at the time of the trigger.</summary>
    [JsonProperty("heartRate")]
    public int HeartRate { get; set; }

    /// <summary>The UUID of the trigger. This is a unique identifier for the trigger event.</summary>
    [JsonProperty("uuid")]
    public Guid Uuid { get; set; }

    /// <summary>The type of trigger. This could be a specific event or condition that caused the trigger.</summary>
    [JsonProperty("triggerType")]
    public string TriggerType { get; set; } // Enum type could be used here

    /// <summary>The target heart rate for the trigger.</summary>
    [JsonProperty("targetHeartRate")]
    public int TargetHeartRate { get; set; }

    /// <summary>The time in seconds the trigger should be satisfied before it is activated.</summary>
    [JsonProperty("holdTime")]
    public int HoldTime { get; set; }

    /// <summary>The duration of the cooldown period for the trigger.</summary>
    [JsonProperty("cooldown")]
    public int Cooldown { get; set; }
}

/// <summary>Details of a Crowd Control test event.</summary>
public class CrowdControlTestSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "crowd-control-test";
}

/// <summary>Details of a Crowd Control chaos mode event.</summary>
public class CrowdControlChaosModeSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "crowd-control-chaos-mode";
}

/// <summary>Details of a Crowd Control retry event.</summary>
public class CrowdControlRetrySourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "crowd-control-retry";
}