namespace ConnectorLib.JSON.Tests;

[TestClass]
public class PlayerInfoTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.PlayerInfo" });
}
