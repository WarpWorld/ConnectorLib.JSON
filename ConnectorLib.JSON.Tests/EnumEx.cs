namespace ConnectorLib.JSON.Tests;

[TestClass]
public class EnumExTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.EnumEx" });
}

[TestClass]
public class ConnectorLib_JSON_EnumExBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.EnumEx", "ToCamelCase", 0, "System.Enum", ReturnTypeName = "System.String", SourcePath = @"EnumEx.cs")]
    [TestMethod]
    public void ToCamelCase() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EnumEx", "ToCamelCase", 0, new[] { "System.Enum" }, "System.String");
}
