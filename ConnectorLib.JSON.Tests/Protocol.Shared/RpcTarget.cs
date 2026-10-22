namespace ConnectorLib.JSON.Tests.ProtocolShared;

[TestClass]
public class RpcTargetTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.RpcTarget" });
}
