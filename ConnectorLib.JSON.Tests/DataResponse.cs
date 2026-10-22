namespace ConnectorLib.JSON.Tests;

[TestClass]
public class DataResponseTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.DataResponse" });
}

[TestClass]
public class ConnectorLib_JSON_DataResponseBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.DataResponse", "SuccessIfDefined", 0, "System.String", "System.Object", "System.String", ReturnTypeName = "ConnectorLib.JSON.DataResponse", SourcePath = @"DataResponse.cs")]
    [TestMethod]
    public void SuccessIfDefined() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.DataResponse", "SuccessIfDefined", 0, new[] { "System.String", "System.Object", "System.String" }, "ConnectorLib.JSON.DataResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.DataResponse", "Success", 0, "System.String", "System.Object", ReturnTypeName = "ConnectorLib.JSON.DataResponse", SourcePath = @"DataResponse.cs")]
    [TestMethod]
    public void Success_System_String_System_Object() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.DataResponse", "Success", 0, new[] { "System.String", "System.Object" }, "ConnectorLib.JSON.DataResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.DataResponse", "Success", 0, "System.String", "System.Object", "System.String", ReturnTypeName = "ConnectorLib.JSON.DataResponse", SourcePath = @"DataResponse.cs")]
    [TestMethod]
    public void Success_System_String_System_Object_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.DataResponse", "Success", 0, new[] { "System.String", "System.Object", "System.String" }, "ConnectorLib.JSON.DataResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.DataResponse", "Failure", 0, "System.String", ReturnTypeName = "ConnectorLib.JSON.DataResponse", SourcePath = @"DataResponse.cs")]
    [TestMethod]
    public void Failure_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.DataResponse", "Failure", 0, new[] { "System.String" }, "ConnectorLib.JSON.DataResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.DataResponse", "Failure", 0, "System.String", "System.String", ReturnTypeName = "ConnectorLib.JSON.DataResponse", SourcePath = @"DataResponse.cs")]
    [TestMethod]
    public void Failure_System_String_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.DataResponse", "Failure", 0, new[] { "System.String", "System.String" }, "ConnectorLib.JSON.DataResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.DataResponse", "Failure", 0, "System.String", "System.Object", "System.String", ReturnTypeName = "ConnectorLib.JSON.DataResponse", SourcePath = @"DataResponse.cs")]
    [TestMethod]
    public void Failure_System_String_System_Object_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.DataResponse", "Failure", 0, new[] { "System.String", "System.Object", "System.String" }, "ConnectorLib.JSON.DataResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.DataResponse", "Retry", 0, "System.String", "System.Int64", "System.String", ReturnTypeName = "ConnectorLib.JSON.DataResponse", SourcePath = @"DataResponse.cs")]
    [TestMethod]
    public void Retry() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.DataResponse", "Retry", 0, new[] { "System.String", "System.Int64", "System.String" }, "ConnectorLib.JSON.DataResponse");
}
