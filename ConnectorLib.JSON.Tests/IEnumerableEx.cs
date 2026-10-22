namespace ConnectorLib.JSON.Tests;

[TestClass]
public class IEnumerableExTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.IEnumerableEx" });
}

[TestClass]
public class ConnectorLib_JSON_IEnumerableExBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.IEnumerableEx", "ToDictionary", 2, "System.Collections.Generic.IEnumerable`1[System.Collections.Generic.KeyValuePair`2[TKey,TValue]]", ReturnTypeName = "System.Collections.Generic.Dictionary`2[TKey,TValue]", SourcePath = @"IEnumerableEx.cs")]
    [TestMethod]
    public void ToDictionary() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.IEnumerableEx", "ToDictionary", 2, new[] { "System.Collections.Generic.IEnumerable`1[System.Collections.Generic.KeyValuePair`2[TKey,TValue]]" }, "System.Collections.Generic.Dictionary`2[TKey,TValue]");
}
