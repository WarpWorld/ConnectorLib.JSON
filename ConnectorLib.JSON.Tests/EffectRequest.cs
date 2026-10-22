namespace ConnectorLib.JSON.Tests;

[TestClass]
public class EffectRequestTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.EffectRequest", "ConnectorLib.JSON.EffectRequest+Target" });
}
