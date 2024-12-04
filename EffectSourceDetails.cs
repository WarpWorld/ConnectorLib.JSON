using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.JSON;

//[JsonConverter(typeof(Converter))]
public interface IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type { get; }

    public class Converter : JsonConverter<IEffectSourceDetails?>
    {
        public static Converter Instance = new();

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

public class TwitchChannelRewardSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "twitch-channel-reward";

    [JsonProperty("rewardID")]
    public string RewardID { get; set; }

    [JsonProperty("redemptionID")]
    public string RedemptionID { get; set; }

    [JsonProperty("twitchID")]
    public string TwitchID { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("cost")]
    public int Cost { get; set; }
}

public class StreamLabsDonationSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "stream-labs-donation";

    [JsonProperty("donationID")]
    public string DonationID { get; set; }

    [JsonProperty("cost")]
    public string Cost { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }
}

public class HypeTrainSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "event-hype-train";

    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("progress")]
    public int Progress { get; set; }

    [JsonProperty("goal")]
    public int Goal { get; set; }

    [JsonProperty("top_contributions")]
    public List<Contribution> TopContributions { get; set; }

    [JsonProperty("last_contribution")]
    public Contribution LastContribution { get; set; }

    [JsonProperty("level")]
    public int Level { get; set; }

    public class Contribution
    {
        [JsonProperty("user_id")]
        public string UserID { get; set; }

        [JsonProperty("user_login")]
        public string UserLogin { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}

public abstract class TikTokSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public abstract string Type { get; }

    [JsonProperty("cost")]
    public int Cost { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("userID")]
    public string UserID { get; set; }
}

public class TikTokGiftSourceDetails : TikTokSourceDetails
{
    [JsonProperty("type")]
    public override string Type =>"tiktok-gift";

    [JsonProperty("giftID")]
    public int GiftID { get; set; }

    [JsonProperty("giftName")]
    public string GiftName { get; set; }

    [JsonProperty("transactionID")]
    public string? TransactionID { get; set; }
}

public class TikTokLikeSourceDetails : TikTokSourceDetails
{
    [JsonProperty("type")]
    public override string Type => "tiktok-like";
}

public class TikTokFollowSourceDetails : TikTokSourceDetails
{
    [JsonProperty("type")]
    public override string Type => "tiktok-follow";
}

public class TikTokShareSourceDetails : TikTokSourceDetails
{
    [JsonProperty("type")]
    public override string Type => "tiktok-share";
}

public class PulsoidTriggerSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "pulsoid-trigger";

    [JsonProperty("heartRate")]
    public int HeartRate { get; set; }

    [JsonProperty("uuid")]
    public string Uuid { get; set; }

    [JsonProperty("triggerType")]
    public string TriggerType { get; set; } // Enum type could be used here

    [JsonProperty("targetHeartRate")]
    public int TargetHeartRate { get; set; }

    [JsonProperty("holdTime")]
    public int HoldTime { get; set; }

    [JsonProperty("cooldown")]
    public int Cooldown { get; set; }
}

public class CrowdControlTestSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "crowd-control-test";
}

public class CrowdControlChaosModeSourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "crowd-control-chaos-mode";
}

public class CrowdControlRetrySourceDetails : IEffectSourceDetails
{
    [JsonProperty("type")]
    public string Type => "crowd-control-retry";
}