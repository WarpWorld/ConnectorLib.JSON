namespace ConnectorLib.JSON.Tests;

[TestClass]
public class SimpleJSONMessageTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.SimpleJSONMessage" });
}

[TestClass]
public class ConnectorLib_JSON_SimpleJSONMessageBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.SimpleJSONMessage", "Serialize", 0, ReturnTypeName = "System.String", SourcePath = @"SimpleJSONMessage.cs")]
    [TestMethod]
    public void Serialize() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.SimpleJSONMessage", "Serialize", 0, Array.Empty<string>(), "System.String");
}
