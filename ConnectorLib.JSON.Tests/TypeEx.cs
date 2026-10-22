namespace ConnectorLib.JSON.Tests;

[TestClass]
public class TypeExTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.TypeEx" });
}

[TestClass]
public class ConnectorLib_JSON_TypeExBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.TypeEx", "IsAssignableTo", 0, "System.Type", "System.Type", ReturnTypeName = "System.Boolean", SourcePath = @"TypeEx.cs")]
    [TestMethod]
    public void IsAssignableTo() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.TypeEx", "IsAssignableTo", 0, new[] { "System.Type", "System.Type" }, "System.Boolean");
}
