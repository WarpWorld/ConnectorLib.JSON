namespace ConnectorLib.JSON.Tests;

[TestClass]
public class HexColorConverterTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.HexColorConverter" });
}

[TestClass]
public class ConnectorLib_JSON_HexColorConverterBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.HexColorConverter", "WriteJson", 0, "Newtonsoft.Json.JsonWriter", "ConnectorLib.JSON.ParameterColorValue", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "System.Void", SourcePath = @"HexColorConverter.cs")]
    [TestMethod]
    public void WriteJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.HexColorConverter", "WriteJson", 0, new[] { "Newtonsoft.Json.JsonWriter", "ConnectorLib.JSON.ParameterColorValue", "Newtonsoft.Json.JsonSerializer" }, "System.Void");

    [MethodBehaviorTest("ConnectorLib.JSON.HexColorConverter", "ReadJson", 0, "Newtonsoft.Json.JsonReader", "System.Type", "ConnectorLib.JSON.ParameterColorValue", "System.Boolean", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "ConnectorLib.JSON.ParameterColorValue", SourcePath = @"HexColorConverter.cs")]
    [TestMethod]
    public void ReadJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.HexColorConverter", "ReadJson", 0, new[] { "Newtonsoft.Json.JsonReader", "System.Type", "ConnectorLib.JSON.ParameterColorValue", "System.Boolean", "Newtonsoft.Json.JsonSerializer" }, "ConnectorLib.JSON.ParameterColorValue");

    [MethodBehaviorTest("ConnectorLib.JSON.HexColorConverter", "TryParse", 0, "System.String", "ConnectorLib.JSON.ParameterColorValue&", ReturnTypeName = "System.Boolean", SourcePath = @"HexColorConverter.cs")]
    [TestMethod]
    public void TryParse() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.HexColorConverter", "TryParse", 0, new[] { "System.String", "ConnectorLib.JSON.ParameterColorValue&" }, "System.Boolean");
}
