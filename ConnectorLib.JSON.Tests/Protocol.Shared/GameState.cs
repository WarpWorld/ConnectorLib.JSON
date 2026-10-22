namespace ConnectorLib.JSON.Tests.ProtocolShared;

[TestClass]
public class GameStateTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.GameState" });
}
