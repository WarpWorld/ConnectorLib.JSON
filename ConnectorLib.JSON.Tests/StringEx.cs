namespace ConnectorLib.JSON.Tests;

[TestClass]
public class StringExTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.StringEx" });
}

[TestClass]
public class ConnectorLib_JSON_StringExBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.StringEx", "IsNullOrWhiteSpace", 0, "System.String", ReturnTypeName = "System.Boolean", SourcePath = @"StringEx.cs")]
    [TestMethod]
    public void IsNullOrWhiteSpace() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.StringEx", "IsNullOrWhiteSpace", 0, new[] { "System.String" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.StringEx", "ToCamelCase", 0, "System.String", ReturnTypeName = "System.String", SourcePath = @"StringEx.cs")]
    [TestMethod]
    public void ToCamelCase() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.StringEx", "ToCamelCase", 0, new[] { "System.String" }, "System.String");

    [MethodBehaviorTest("ConnectorLib.JSON.StringEx", "Chop", 0, "System.String", "System.Int32", ReturnTypeName = "System.String[]", SourcePath = @"StringEx.cs")]
    [TestMethod]
    public void Chop() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.StringEx", "Chop", 0, new[] { "System.String", "System.Int32" }, "System.String[]");
}
