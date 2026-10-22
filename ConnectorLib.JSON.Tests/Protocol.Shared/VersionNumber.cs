namespace ConnectorLib.JSON.Tests.ProtocolShared;

[TestClass]
public class VersionNumberTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber+CollectableLoadContext", "ConnectorLib.JSON.VersionNumber+Converter", "ConnectorLib.JSON.IEffectSourceDetails+Converter", "ConnectorLib.JSON.RequestParameters+Converter" });
}

[TestClass]
public class ConnectorLib_JSON_VersionNumberBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, "System.String", ReturnTypeName = "ConnectorLib.JSON.VersionNumber", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_Implicit_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, new[] { "System.String" }, "ConnectorLib.JSON.VersionNumber");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, "System.UInt32", ReturnTypeName = "ConnectorLib.JSON.VersionNumber", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_Implicit_System_UInt32() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, new[] { "System.UInt32" }, "ConnectorLib.JSON.VersionNumber");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, "System.UInt32[]", ReturnTypeName = "ConnectorLib.JSON.VersionNumber", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_Implicit_System_UInt32__() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, new[] { "System.UInt32[]" }, "ConnectorLib.JSON.VersionNumber");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, "System.Span`1[System.UInt32]", ReturnTypeName = "ConnectorLib.JSON.VersionNumber", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_Implicit_System_Span_1_System_UInt32_()
    {
        Span<uint> components = new uint[] { 1, 2, 3 };
        ConnectorLib.JSON.VersionNumber version = components;
        Assert.AreEqual("1.2.3", version.ToString());
    }

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, "System.Collections.Generic.List`1[System.UInt32]", ReturnTypeName = "ConnectorLib.JSON.VersionNumber", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_Implicit_System_Collections_Generic_List_1_System_UInt32_() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, new[] { "System.Collections.Generic.List`1[System.UInt32]" }, "ConnectorLib.JSON.VersionNumber");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, "ConnectorLib.JSON.VersionNumber", ReturnTypeName = "System.String", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_Implicit_ConnectorLib_JSON_VersionNumber_Returns_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, new[] { "ConnectorLib.JSON.VersionNumber" }, "System.String");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, "ConnectorLib.JSON.VersionNumber", ReturnTypeName = "System.UInt32[]", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_Implicit_ConnectorLib_JSON_VersionNumber_Returns_System_UInt32__() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "op_Implicit", 0, new[] { "ConnectorLib.JSON.VersionNumber" }, "System.UInt32[]");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_Equality", 0, "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber", ReturnTypeName = "System.Boolean", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_Equality() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "op_Equality", 0, new[] { "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_Inequality", 0, "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber", ReturnTypeName = "System.Boolean", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_Inequality() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "op_Inequality", 0, new[] { "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_LessThan", 0, "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber", ReturnTypeName = "System.Boolean", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_LessThan() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "op_LessThan", 0, new[] { "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_GreaterThan", 0, "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber", ReturnTypeName = "System.Boolean", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_GreaterThan() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "op_GreaterThan", 0, new[] { "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_LessThanOrEqual", 0, "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber", ReturnTypeName = "System.Boolean", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_LessThanOrEqual() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "op_LessThanOrEqual", 0, new[] { "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "op_GreaterThanOrEqual", 0, "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber", ReturnTypeName = "System.Boolean", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void op_GreaterThanOrEqual() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "op_GreaterThanOrEqual", 0, new[] { "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "ToString", 0, ReturnTypeName = "System.String", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void ToString() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "ToString", 0, Array.Empty<string>(), "System.String");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "Equals", 0, "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber", ReturnTypeName = "System.Boolean", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void Equals_ConnectorLib_JSON_VersionNumber_ConnectorLib_JSON_VersionNumber() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "Equals", 0, new[] { "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "Equals", 0, "System.Object", ReturnTypeName = "System.Boolean", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void Equals_System_Object() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "Equals", 0, new[] { "System.Object" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "GetHashCode", 0, ReturnTypeName = "System.Int32", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void GetHashCode() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "GetHashCode", 0, Array.Empty<string>(), "System.Int32");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "Equals", 0, "ConnectorLib.JSON.VersionNumber", ReturnTypeName = "System.Boolean", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void Equals_ConnectorLib_JSON_VersionNumber() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "Equals", 0, new[] { "ConnectorLib.JSON.VersionNumber" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "CompareTo", 0, "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber", ReturnTypeName = "System.Int32", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void CompareTo_ConnectorLib_JSON_VersionNumber_ConnectorLib_JSON_VersionNumber() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "CompareTo", 0, new[] { "ConnectorLib.JSON.VersionNumber", "ConnectorLib.JSON.VersionNumber" }, "System.Int32");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "CompareTo", 0, "ConnectorLib.JSON.VersionNumber", ReturnTypeName = "System.Int32", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void CompareTo_ConnectorLib_JSON_VersionNumber() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "CompareTo", 0, new[] { "ConnectorLib.JSON.VersionNumber" }, "System.Int32");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "GetAssemblyByName", 0, "System.String", "System.String", ReturnTypeName = "System.Reflection.Assembly", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void GetAssemblyByName() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "GetAssemblyByName", 0, new[] { "System.String", "System.String" }, "System.Reflection.Assembly");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "GetPackAssemblies", 0, "System.String", ReturnTypeName = "System.Collections.Generic.IEnumerable`1[System.Reflection.Assembly]", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void GetPackAssemblies() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "GetPackAssemblies", 0, new[] { "System.String" }, "System.Collections.Generic.IEnumerable`1[System.Reflection.Assembly]");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "GetPackVersion", 0, "System.String", ReturnTypeName = "ConnectorLib.JSON.VersionNumber", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void GetPackVersion() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "GetPackVersion", 0, new[] { "System.String" }, "ConnectorLib.JSON.VersionNumber");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "GetAssemblyVersion", 0, "System.Collections.Generic.IEnumerable`1[System.Reflection.Assembly]", ReturnTypeName = "ConnectorLib.JSON.VersionNumber", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void GetAssemblyVersion_System_Collections_Generic_IEnumerable_1_System_Reflection_Assembly_() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "GetAssemblyVersion", 0, new[] { "System.Collections.Generic.IEnumerable`1[System.Reflection.Assembly]" }, "ConnectorLib.JSON.VersionNumber");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "GetAssemblyVersion", 0, "System.Reflection.Assembly", ReturnTypeName = "ConnectorLib.JSON.VersionNumber", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void GetAssemblyVersion_System_Reflection_Assembly() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "GetAssemblyVersion", 0, new[] { "System.Reflection.Assembly" }, "ConnectorLib.JSON.VersionNumber");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "Sanitize", 0, "System.String", ReturnTypeName = "System.String", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void Sanitize() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "Sanitize", 0, new[] { "System.String" }, "System.String");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber", "GetFileVersionInfo", 0, "System.Reflection.Assembly", ReturnTypeName = "System.Diagnostics.FileVersionInfo", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void GetFileVersionInfo() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber", "GetFileVersionInfo", 0, new[] { "System.Reflection.Assembly" }, "System.Diagnostics.FileVersionInfo");
}

[TestClass]
public class ConnectorLib_JSON_VersionNumber_CollectableLoadContextBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber+CollectableLoadContext", "LoadFrom", 0, "System.String", ReturnTypeName = "System.Reflection.Assembly", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void LoadFrom() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber+CollectableLoadContext", "LoadFrom", 0, new[] { "System.String" }, "System.Reflection.Assembly");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber+CollectableLoadContext", "Load", 0, "System.String", ReturnTypeName = "System.Reflection.Assembly", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void Load() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber+CollectableLoadContext", "Load", 0, new[] { "System.String" }, "System.Reflection.Assembly");
}

[TestClass]
public class ConnectorLib_JSON_VersionNumber_ConverterBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber+Converter", "WriteJson", 0, "Newtonsoft.Json.JsonWriter", "ConnectorLib.JSON.VersionNumber", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "System.Void", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void WriteJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber+Converter", "WriteJson", 0, new[] { "Newtonsoft.Json.JsonWriter", "ConnectorLib.JSON.VersionNumber", "Newtonsoft.Json.JsonSerializer" }, "System.Void");

    [MethodBehaviorTest("ConnectorLib.JSON.VersionNumber+Converter", "ReadJson", 0, "Newtonsoft.Json.JsonReader", "System.Type", "ConnectorLib.JSON.VersionNumber", "System.Boolean", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "ConnectorLib.JSON.VersionNumber", SourcePath = @"Protocol.Shared\VersionNumber.cs")]
    [TestMethod]
    public void ReadJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.VersionNumber+Converter", "ReadJson", 0, new[] { "Newtonsoft.Json.JsonReader", "System.Type", "ConnectorLib.JSON.VersionNumber", "System.Boolean", "Newtonsoft.Json.JsonSerializer" }, "ConnectorLib.JSON.VersionNumber");
}
