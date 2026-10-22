namespace ConnectorLib.JSON.Tests;

[TestClass]
public class SimpleJSONResponseTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.SimpleJSONResponse" });
}

[TestClass]
public class ConnectorLib_JSON_SimpleJSONResponseBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.SimpleJSONResponse", "TryParse", 0, "System.String", "ConnectorLib.JSON.SimpleJSONResponse&", ReturnTypeName = "System.Boolean", SourcePath = @"SimpleJSONResponse.cs")]
    [TestMethod]
    public void TryParse_System_String_ConnectorLib_JSON_SimpleJSONResponse_() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.SimpleJSONResponse", "TryParse", 0, new[] { "System.String", "ConnectorLib.JSON.SimpleJSONResponse&" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.SimpleJSONResponse", "TryParse", 0, "Newtonsoft.Json.Linq.JObject", "ConnectorLib.JSON.SimpleJSONResponse&", ReturnTypeName = "System.Boolean", SourcePath = @"SimpleJSONResponse.cs")]
    [TestMethod]
    public void TryParse_Newtonsoft_Json_Linq_JObject_ConnectorLib_JSON_SimpleJSONResponse_() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.SimpleJSONResponse", "TryParse", 0, new[] { "Newtonsoft.Json.Linq.JObject", "ConnectorLib.JSON.SimpleJSONResponse&" }, "System.Boolean");
}
