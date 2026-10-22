namespace ConnectorLib.JSON.Tests;

[TestClass]
public class CamelCaseStringEnumConverterTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.CamelCaseStringEnumConverter" });
}

[TestClass]
public class ConnectorLib_JSON_CamelCaseStringEnumConverterBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.CamelCaseStringEnumConverter", "WriteJson", 0, "Newtonsoft.Json.JsonWriter", "System.Enum", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "System.Void", SourcePath = @"CamelCaseStringEnumConverter.cs")]
    [TestMethod]
    public void WriteJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.CamelCaseStringEnumConverter", "WriteJson", 0, new[] { "Newtonsoft.Json.JsonWriter", "System.Enum", "Newtonsoft.Json.JsonSerializer" }, "System.Void");

    [MethodBehaviorTest("ConnectorLib.JSON.CamelCaseStringEnumConverter", "ReadJson", 0, "Newtonsoft.Json.JsonReader", "System.Type", "System.Enum", "System.Boolean", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "System.Enum", SourcePath = @"CamelCaseStringEnumConverter.cs")]
    [TestMethod]
    public void ReadJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.CamelCaseStringEnumConverter", "ReadJson", 0, new[] { "Newtonsoft.Json.JsonReader", "System.Type", "System.Enum", "System.Boolean", "Newtonsoft.Json.JsonSerializer" }, "System.Enum");

    [MethodBehaviorTest("ConnectorLib.JSON.CamelCaseStringEnumConverter", "ReadJToken", 0, "Newtonsoft.Json.Linq.JToken", "System.Type", ReturnTypeName = "System.Enum", SourcePath = @"CamelCaseStringEnumConverter.cs")]
    [TestMethod]
    public void ReadJToken() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.CamelCaseStringEnumConverter", "ReadJToken", 0, new[] { "Newtonsoft.Json.Linq.JToken", "System.Type" }, "System.Enum");
}
