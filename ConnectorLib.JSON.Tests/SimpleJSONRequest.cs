namespace ConnectorLib.JSON.Tests;

[TestClass]
public class SimpleJSONRequestTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.SimpleJSONRequest" });
}

[TestClass]
public class ConnectorLib_JSON_SimpleJSONRequestBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.SimpleJSONRequest", "TryParse", 0, "System.String", "ConnectorLib.JSON.SimpleJSONRequest&", ReturnTypeName = "System.Boolean", SourcePath = @"SimpleJSONRequest.cs")]
    [TestMethod]
    public void TryParse_System_String_ConnectorLib_JSON_SimpleJSONRequest_() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.SimpleJSONRequest", "TryParse", 0, new[] { "System.String", "ConnectorLib.JSON.SimpleJSONRequest&" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.SimpleJSONRequest", "TryParse", 0, "Newtonsoft.Json.Linq.JObject", "ConnectorLib.JSON.SimpleJSONRequest&", ReturnTypeName = "System.Boolean", SourcePath = @"SimpleJSONRequest.cs")]
    [TestMethod]
    public void TryParse_Newtonsoft_Json_Linq_JObject_ConnectorLib_JSON_SimpleJSONRequest_() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.SimpleJSONRequest", "TryParse", 0, new[] { "Newtonsoft.Json.Linq.JObject", "ConnectorLib.JSON.SimpleJSONRequest&" }, "System.Boolean");
}
