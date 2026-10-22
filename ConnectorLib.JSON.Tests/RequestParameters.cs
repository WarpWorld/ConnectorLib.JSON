namespace ConnectorLib.JSON.Tests;

[TestClass]
public class RequestParametersTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.RequestParameters", "ConnectorLib.JSON.RequestParameters+Converter", "ConnectorLib.JSON.IEffectSourceDetails+Converter", "ConnectorLib.JSON.VersionNumber+Converter" });
}

[TestClass]
public class ConnectorLib_JSON_RequestParametersBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,ConnectorLib.JSON.IParameterValue>>.GetEnumerator", 0, ReturnTypeName = "System.Collections.Generic.IEnumerator`1[System.Collections.Generic.KeyValuePair`2[System.String,ConnectorLib.JSON.IParameterValue]]", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void GetEnumerator_System_Collections_Generic_IEnumerable_System_Collections_Generic_KeyValuePair_System_String_ConnectorLib_JSON_IParameterValue___() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,ConnectorLib.JSON.IParameterValue>>.GetEnumerator", 0, Array.Empty<string>(), "System.Collections.Generic.IEnumerator`1[System.Collections.Generic.KeyValuePair`2[System.String,ConnectorLib.JSON.IParameterValue]]");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "System.Collections.Generic.IEnumerable<System.String>.GetEnumerator", 0, ReturnTypeName = "System.Collections.Generic.IEnumerator`1[System.String]", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void GetEnumerator_System_Collections_Generic_IEnumerable_System_String__() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "System.Collections.Generic.IEnumerable<System.String>.GetEnumerator", 0, Array.Empty<string>(), "System.Collections.Generic.IEnumerator`1[System.String]");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "System.Collections.IEnumerable.GetEnumerator", 0, ReturnTypeName = "System.Collections.IEnumerator", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void GetEnumerator_System_Collections_IEnumerable_() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "System.Collections.IEnumerable.GetEnumerator", 0, Array.Empty<string>(), "System.Collections.IEnumerator");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "ContainsKey", 0, "System.String", ReturnTypeName = "System.Boolean", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void ContainsKey() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "ContainsKey", 0, new[] { "System.String" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "TryGetValue", 0, "System.String", "ConnectorLib.JSON.IParameterValue&", ReturnTypeName = "System.Boolean", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void TryGetValue() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "TryGetValue", 0, new[] { "System.String", "ConnectorLib.JSON.IParameterValue&" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "Where", 0, "System.Func`2[System.String,System.Boolean]", ReturnTypeName = "System.Collections.Generic.IEnumerable`1[System.String]", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void Where() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "Where", 0, new[] { "System.Func`2[System.String,System.Boolean]" }, "System.Collections.Generic.IEnumerable`1[System.String]");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "Select", 1, "System.Func`2[System.String,TResult]", ReturnTypeName = "System.Collections.Generic.IEnumerable`1[TResult]", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void Select() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "Select", 1, new[] { "System.Func`2[System.String,TResult]" }, "System.Collections.Generic.IEnumerable`1[TResult]");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "SelectMany", 1, "System.Func`2[System.String,System.Collections.Generic.IEnumerable`1[TResult]]", ReturnTypeName = "System.Collections.Generic.IEnumerable`1[TResult]", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void SelectMany_Generic1_System_Func_2_System_String_System_Collections_Generic_IEnumerable_1_TResult__() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "SelectMany", 1, new[] { "System.Func`2[System.String,System.Collections.Generic.IEnumerable`1[TResult]]" }, "System.Collections.Generic.IEnumerable`1[TResult]");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "SelectMany", 1, "System.Func`3[System.String,System.Int32,System.Collections.Generic.IEnumerable`1[TResult]]", ReturnTypeName = "System.Collections.Generic.IEnumerable`1[TResult]", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void SelectMany_Generic1_System_Func_3_System_String_System_Int32_System_Collections_Generic_IEnumerable_1_TResult__() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "SelectMany", 1, new[] { "System.Func`3[System.String,System.Int32,System.Collections.Generic.IEnumerable`1[TResult]]" }, "System.Collections.Generic.IEnumerable`1[TResult]");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "First", 0, ReturnTypeName = "System.String", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void First_NoParameters() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "First", 0, Array.Empty<string>(), "System.String");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "First", 0, "System.Func`2[System.String,System.Boolean]", ReturnTypeName = "System.String", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void First_System_Func_2_System_String_System_Boolean_() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "First", 0, new[] { "System.Func`2[System.String,System.Boolean]" }, "System.String");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "FirstOrDefault", 0, ReturnTypeName = "System.String", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void FirstOrDefault_NoParameters() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "FirstOrDefault", 0, Array.Empty<string>(), "System.String");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "FirstOrDefault", 0, "System.String", ReturnTypeName = "System.String", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void FirstOrDefault_System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "FirstOrDefault", 0, new[] { "System.String" }, "System.String");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "FirstOrDefault", 0, "System.Func`2[System.String,System.Boolean]", ReturnTypeName = "System.String", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void FirstOrDefault_System_Func_2_System_String_System_Boolean_() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "FirstOrDefault", 0, new[] { "System.Func`2[System.String,System.Boolean]" }, "System.String");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "FirstOrDefault", 0, "System.Func`2[System.String,System.Boolean]", "System.String", ReturnTypeName = "System.String", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void FirstOrDefault_System_Func_2_System_String_System_Boolean__System_String() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "FirstOrDefault", 0, new[] { "System.Func`2[System.String,System.Boolean]", "System.String" }, "System.String");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "Any", 0, ReturnTypeName = "System.Boolean", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void Any_NoParameters() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "Any", 0, Array.Empty<string>(), "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters", "Any", 0, "System.Func`2[System.String,System.Boolean]", ReturnTypeName = "System.Boolean", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void Any_System_Func_2_System_String_System_Boolean_() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters", "Any", 0, new[] { "System.Func`2[System.String,System.Boolean]" }, "System.Boolean");
}

[TestClass]
public class ConnectorLib_JSON_RequestParameters_ConverterBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters+Converter", "WriteJson", 0, "Newtonsoft.Json.JsonWriter", "ConnectorLib.JSON.RequestParameters", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "System.Void", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void WriteJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters+Converter", "WriteJson", 0, new[] { "Newtonsoft.Json.JsonWriter", "ConnectorLib.JSON.RequestParameters", "Newtonsoft.Json.JsonSerializer" }, "System.Void");

    [MethodBehaviorTest("ConnectorLib.JSON.RequestParameters+Converter", "ReadJson", 0, "Newtonsoft.Json.JsonReader", "System.Type", "ConnectorLib.JSON.RequestParameters", "System.Boolean", "Newtonsoft.Json.JsonSerializer", ReturnTypeName = "ConnectorLib.JSON.RequestParameters", SourcePath = @"RequestParameters.cs")]
    [TestMethod]
    public void ReadJson() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.RequestParameters+Converter", "ReadJson", 0, new[] { "Newtonsoft.Json.JsonReader", "System.Type", "ConnectorLib.JSON.RequestParameters", "System.Boolean", "Newtonsoft.Json.JsonSerializer" }, "ConnectorLib.JSON.RequestParameters");
}
