namespace ConnectorLib.JSON.Tests;

[TestClass]
public class EffectResponseTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.EffectResponse", "ConnectorLib.JSON.EffectResponse+MetadataConverter" });
}

[TestClass]
public class ConnectorLib_JSON_EffectResponseBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Success", 0, "System.UInt32", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Success_System_UInt32_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Success", 0, new[] { "System.UInt32", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Success", 0, "System.UInt32", "System.Int64", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Success_System_UInt32_System_Int64_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Success", 0, new[] { "System.UInt32", "System.Int64", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Success", 0, "System.UInt32", "System.TimeSpan", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Success_System_UInt32_System_TimeSpan_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Success", 0, new[] { "System.UInt32", "System.TimeSpan", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Failure", 0, "System.UInt32", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Failure_System_UInt32_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Failure", 0, new[] { "System.UInt32", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Failure", 0, "System.UInt32", "ConnectorLib.JSON.StandardErrors", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Failure_System_UInt32_ConnectorLib_JSON_StandardErrors_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Failure", 0, new[] { "System.UInt32", "ConnectorLib.JSON.StandardErrors", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Unavailable", 0, "System.UInt32", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Unavailable_System_UInt32_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Unavailable", 0, new[] { "System.UInt32", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Unavailable", 0, "System.UInt32", "ConnectorLib.JSON.StandardErrors", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Unavailable_System_UInt32_ConnectorLib_JSON_StandardErrors() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Unavailable", 0, new[] { "System.UInt32", "ConnectorLib.JSON.StandardErrors" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Retry", 0, "System.UInt32", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Retry_System_UInt32_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Retry", 0, new[] { "System.UInt32", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Retry", 0, "System.UInt32", "System.Int64", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Retry_System_UInt32_System_Int64_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Retry", 0, new[] { "System.UInt32", "System.Int64", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Retry", 0, "System.UInt32", "System.TimeSpan", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Retry_System_UInt32_System_TimeSpan_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Retry", 0, new[] { "System.UInt32", "System.TimeSpan", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Retry", 0, "System.UInt32", "ConnectorLib.JSON.StandardErrors", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Retry_System_UInt32_ConnectorLib_JSON_StandardErrors() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Retry", 0, new[] { "System.UInt32", "ConnectorLib.JSON.StandardErrors" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Retry", 0, "System.UInt32", "System.Int64", "ConnectorLib.JSON.StandardErrors", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Retry_System_UInt32_System_Int64_ConnectorLib_JSON_StandardErrors() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Retry", 0, new[] { "System.UInt32", "System.Int64", "ConnectorLib.JSON.StandardErrors" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Retry", 0, "System.UInt32", "System.TimeSpan", "ConnectorLib.JSON.StandardErrors", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Retry_System_UInt32_System_TimeSpan_ConnectorLib_JSON_StandardErrors() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Retry", 0, new[] { "System.UInt32", "System.TimeSpan", "ConnectorLib.JSON.StandardErrors" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Paused", 0, "System.UInt32", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Paused_System_UInt32_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Paused", 0, new[] { "System.UInt32", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Paused", 0, "System.UInt32", "System.Int64", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Paused_System_UInt32_System_Int64_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Paused", 0, new[] { "System.UInt32", "System.Int64", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Paused", 0, "System.UInt32", "System.TimeSpan", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Paused_System_UInt32_System_TimeSpan_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Paused", 0, new[] { "System.UInt32", "System.TimeSpan", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Paused", 0, "System.UInt32", "ConnectorLib.JSON.StandardErrors", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Paused_System_UInt32_ConnectorLib_JSON_StandardErrors() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Paused", 0, new[] { "System.UInt32", "ConnectorLib.JSON.StandardErrors" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Paused", 0, "System.UInt32", "System.Int64", "ConnectorLib.JSON.StandardErrors", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Paused_System_UInt32_System_Int64_ConnectorLib_JSON_StandardErrors() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Paused", 0, new[] { "System.UInt32", "System.Int64", "ConnectorLib.JSON.StandardErrors" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Paused", 0, "System.UInt32", "System.TimeSpan", "ConnectorLib.JSON.StandardErrors", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Paused_System_UInt32_System_TimeSpan_ConnectorLib_JSON_StandardErrors() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Paused", 0, new[] { "System.UInt32", "System.TimeSpan", "ConnectorLib.JSON.StandardErrors" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Resumed", 0, "System.UInt32", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Resumed_System_UInt32_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Resumed", 0, new[] { "System.UInt32", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Resumed", 0, "System.UInt32", "System.Int64", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Resumed_System_UInt32_System_Int64_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Resumed", 0, new[] { "System.UInt32", "System.Int64", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Resumed", 0, "System.UInt32", "System.TimeSpan", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Resumed_System_UInt32_System_TimeSpan_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Resumed", 0, new[] { "System.UInt32", "System.TimeSpan", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Resumed", 0, "System.UInt32", "ConnectorLib.JSON.StandardErrors", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Resumed_System_UInt32_ConnectorLib_JSON_StandardErrors() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Resumed", 0, new[] { "System.UInt32", "ConnectorLib.JSON.StandardErrors" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Resumed", 0, "System.UInt32", "System.Int64", "ConnectorLib.JSON.StandardErrors", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Resumed_System_UInt32_System_Int64_ConnectorLib_JSON_StandardErrors() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Resumed", 0, new[] { "System.UInt32", "System.Int64", "ConnectorLib.JSON.StandardErrors" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Resumed", 0, "System.UInt32", "System.TimeSpan", "ConnectorLib.JSON.StandardErrors", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Resumed_System_UInt32_System_TimeSpan_ConnectorLib_JSON_StandardErrors() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Resumed", 0, new[] { "System.UInt32", "System.TimeSpan", "ConnectorLib.JSON.StandardErrors" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Finished", 0, "System.UInt32", "System.String", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Finished_System_UInt32_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Finished", 0, new[] { "System.UInt32", "System.String" }, "ConnectorLib.JSON.EffectResponse");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse", "Finished", 0, "System.UInt32", "ConnectorLib.JSON.StandardErrors", ReturnTypeName = "ConnectorLib.JSON.EffectResponse", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void Finished_System_UInt32_ConnectorLib_JSON_StandardErrors() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse", "Finished", 0, new[] { "System.UInt32", "ConnectorLib.JSON.StandardErrors" }, "ConnectorLib.JSON.EffectResponse");
}

[TestClass]
public class ConnectorLib_JSON_EffectResponse_MetadataConverterBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse+MetadataConverter", "WriteJson", 0, "Newtonsoft.Json.JsonWriter", "System.Collections.Generic.Dictionary`2[System.String,ConnectorLib.JSON.DataResponse]", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "System.Void", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void WriteJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse+MetadataConverter", "WriteJson", 0, new[] { "Newtonsoft.Json.JsonWriter", "System.Collections.Generic.Dictionary`2[System.String,ConnectorLib.JSON.DataResponse]", "Newtonsoft.Json.JsonSerializer" }, "System.Void");

    [MethodBehaviorTest("ConnectorLib.JSON.EffectResponse+MetadataConverter", "ReadJson", 0, "Newtonsoft.Json.JsonReader", "System.Type", "System.Collections.Generic.Dictionary`2[System.String,ConnectorLib.JSON.DataResponse]", "System.Boolean", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "System.Collections.Generic.Dictionary`2[System.String,ConnectorLib.JSON.DataResponse]", SourcePath = @"EffectResponse.cs")]
    [TestMethod]
    public void ReadJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.EffectResponse+MetadataConverter", "ReadJson", 0, new[] { "Newtonsoft.Json.JsonReader", "System.Type", "System.Collections.Generic.Dictionary`2[System.String,ConnectorLib.JSON.DataResponse]", "System.Boolean", "Newtonsoft.Json.JsonSerializer" }, "System.Collections.Generic.Dictionary`2[System.String,ConnectorLib.JSON.DataResponse]");
}
