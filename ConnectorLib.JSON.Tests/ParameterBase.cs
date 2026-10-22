namespace ConnectorLib.JSON.Tests;

[TestClass]
public class ParameterBaseTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.ParameterBase", "ConnectorLib.JSON.ParameterBase+ParameterType", "ConnectorLib.JSON.ParameterBase+ParameterTypeConverter", "ConnectorLib.JSON.IEffectSourceDetails+Converter", "ConnectorLib.JSON.RequestParameters+Converter", "ConnectorLib.JSON.VersionNumber+Converter" });
}

[TestClass]
public class ConnectorLib_JSON_ParameterBase_ParameterTypeConverterBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterBase+ParameterTypeConverter", "WriteJson", 0, "Newtonsoft.Json.JsonWriter", "ConnectorLib.JSON.ParameterBase+ParameterType", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "System.Void", SourcePath = @"ParameterBase.cs")]
    [TestMethod]
    public void WriteJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterBase+ParameterTypeConverter", "WriteJson", 0, new[] { "Newtonsoft.Json.JsonWriter", "ConnectorLib.JSON.ParameterBase+ParameterType", "Newtonsoft.Json.JsonSerializer" }, "System.Void");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterBase+ParameterTypeConverter", "ReadJson", 0, "Newtonsoft.Json.JsonReader", "System.Type", "ConnectorLib.JSON.ParameterBase+ParameterType", "System.Boolean", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "ConnectorLib.JSON.ParameterBase+ParameterType", SourcePath = @"ParameterBase.cs")]
    [TestMethod]
    public void ReadJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterBase+ParameterTypeConverter", "ReadJson", 0, new[] { "Newtonsoft.Json.JsonReader", "System.Type", "ConnectorLib.JSON.ParameterBase+ParameterType", "System.Boolean", "Newtonsoft.Json.JsonSerializer" }, "ConnectorLib.JSON.ParameterBase+ParameterType");
}
