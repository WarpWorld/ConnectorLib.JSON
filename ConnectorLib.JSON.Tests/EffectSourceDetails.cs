namespace ConnectorLib.JSON.Tests;

[TestClass]
public class EffectSourceDetailsTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.IEffectSourceDetails", "ConnectorLib.JSON.TwitchChannelRewardSourceDetails", "ConnectorLib.JSON.StreamLabsDonationSourceDetails", "ConnectorLib.JSON.HypeTrainSourceDetails", "ConnectorLib.JSON.TikTokSourceDetails", "ConnectorLib.JSON.TikTokGiftSourceDetails", "ConnectorLib.JSON.TikTokLikeSourceDetails", "ConnectorLib.JSON.TikTokFollowSourceDetails", "ConnectorLib.JSON.TikTokShareSourceDetails", "ConnectorLib.JSON.PulsoidTriggerSourceDetails", "ConnectorLib.JSON.CrowdControlTestSourceDetails", "ConnectorLib.JSON.CrowdControlChaosModeSourceDetails", "ConnectorLib.JSON.CrowdControlRetrySourceDetails", "ConnectorLib.JSON.IEffectSourceDetails+Converter", "ConnectorLib.JSON.HypeTrainSourceDetails+Contribution", "ConnectorLib.JSON.RequestParameters+Converter", "ConnectorLib.JSON.VersionNumber+Converter" });
}

[TestClass]
public class ConnectorLib_JSON_IEffectSourceDetails_ConverterBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.IEffectSourceDetails+Converter", "ReadJson", 0, "Newtonsoft.Json.JsonReader", "System.Type", "ConnectorLib.JSON.IEffectSourceDetails", "System.Boolean", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "ConnectorLib.JSON.IEffectSourceDetails", SourcePath = @"EffectSourceDetails.cs")]
    [TestMethod]
    public void ReadJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.IEffectSourceDetails+Converter", "ReadJson", 0, new[] { "Newtonsoft.Json.JsonReader", "System.Type", "ConnectorLib.JSON.IEffectSourceDetails", "System.Boolean", "Newtonsoft.Json.JsonSerializer" }, "ConnectorLib.JSON.IEffectSourceDetails");

    [MethodBehaviorTest("ConnectorLib.JSON.IEffectSourceDetails+Converter", "WriteJson", 0, "Newtonsoft.Json.JsonWriter", "ConnectorLib.JSON.IEffectSourceDetails", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "System.Void", SourcePath = @"EffectSourceDetails.cs")]
    [TestMethod]
    public void WriteJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.IEffectSourceDetails+Converter", "WriteJson", 0, new[] { "Newtonsoft.Json.JsonWriter", "ConnectorLib.JSON.IEffectSourceDetails", "Newtonsoft.Json.JsonSerializer" }, "System.Void");
}
