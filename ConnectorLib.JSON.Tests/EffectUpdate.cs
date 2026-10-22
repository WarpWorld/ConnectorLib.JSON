namespace ConnectorLib.JSON.Tests;

[TestClass]
public class EffectUpdateTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.EffectUpdate", "ConnectorLib.JSON.EffectUpdate+IdentifierType" });
}
