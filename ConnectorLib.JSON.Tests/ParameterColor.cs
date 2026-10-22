namespace ConnectorLib.JSON.Tests;

[TestClass]
public class ParameterColorTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.ParameterColor" });
}
