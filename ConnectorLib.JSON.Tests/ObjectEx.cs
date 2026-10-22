namespace ConnectorLib.JSON.Tests;

[TestClass]
public class ObjectExTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.ObjectEx" });
}

[TestClass]
public class ConnectorLib_JSON_ObjectExBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.ObjectEx", "IfNotNull", 2, "T1", "System.Func`2[T1,T2]", ReturnTypeName = "T2", SourcePath = @"ObjectEx.cs")]
    [TestMethod]
    public void IfNotNull() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ObjectEx", "IfNotNull", 2, new[] { "T1", "System.Func`2[T1,T2]" }, "T2");
}
