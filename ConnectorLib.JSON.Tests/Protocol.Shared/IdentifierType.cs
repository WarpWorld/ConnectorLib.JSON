namespace ConnectorLib.JSON.Tests.ProtocolShared;

[TestClass]
public class IdentifierTypeTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.EffectUpdate", "ConnectorLib.JSON.EffectUpdate+IdentifierType" });
}
