namespace ConnectorLib.JSON.Tests;

[TestClass]
public class ParameterValueTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.ParameterValue`1" });
}

[TestClass]
public class ConnectorLib_JSON_ParameterValue_1BehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterValue`1", "ToString", 0, ReturnTypeName = "System.String", SourcePath = @"ParameterValue.cs")]
    [TestMethod]
    public void ToString() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterValue`1", "ToString", 0, Array.Empty<string>(), "System.String");
}
