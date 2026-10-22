namespace ConnectorLib.JSON.Tests;

[TestClass]
public class ParameterColorValueTests
{
    [TestMethod]
    public void SourceTypesExposeExpectedBehavior() =>
        MethodBehaviorAssertions.AssertSourceTypeBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, new[] { "ConnectorLib.JSON.ParameterColorValue" });
}

[TestClass]
public class ConnectorLib_JSON_ParameterColorValueBehaviorTests
{

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "FromArgb", 0, "System.UInt32", ReturnTypeName = "ConnectorLib.JSON.ParameterColorValue", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void FromArgb_System_UInt32() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "FromArgb", 0, new[] { "System.UInt32" }, "ConnectorLib.JSON.ParameterColorValue");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "FromArgb", 0, "System.Int32", ReturnTypeName = "ConnectorLib.JSON.ParameterColorValue", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void FromArgb_System_Int32() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "FromArgb", 0, new[] { "System.Int32" }, "ConnectorLib.JSON.ParameterColorValue");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "FromArgb", 0, "System.Byte", "System.Byte", "System.Byte", "System.Byte", ReturnTypeName = "ConnectorLib.JSON.ParameterColorValue", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void FromArgb_System_Byte_System_Byte_System_Byte_System_Byte() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "FromArgb", 0, new[] { "System.Byte", "System.Byte", "System.Byte", "System.Byte" }, "ConnectorLib.JSON.ParameterColorValue");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "FromArgb", 0, "System.Int32", "ConnectorLib.JSON.ParameterColorValue", ReturnTypeName = "ConnectorLib.JSON.ParameterColorValue", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void FromArgb_System_Int32_ConnectorLib_JSON_ParameterColorValue() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "FromArgb", 0, new[] { "System.Int32", "ConnectorLib.JSON.ParameterColorValue" }, "ConnectorLib.JSON.ParameterColorValue");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "FromArgb", 0, "System.Byte", "System.Byte", "System.Byte", ReturnTypeName = "ConnectorLib.JSON.ParameterColorValue", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void FromArgb_System_Byte_System_Byte_System_Byte() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "FromArgb", 0, new[] { "System.Byte", "System.Byte", "System.Byte" }, "ConnectorLib.JSON.ParameterColorValue");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "GetRgbValues", 0, "System.Byte&", "System.Byte&", "System.Byte&", ReturnTypeName = "System.Void", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void GetRgbValues() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "GetRgbValues", 0, new[] { "System.Byte&", "System.Byte&", "System.Byte&" }, "System.Void");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "GetBrightness", 0, ReturnTypeName = "System.Single", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void GetBrightness() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "GetBrightness", 0, Array.Empty<string>(), "System.Single");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "GetHue", 0, ReturnTypeName = "System.Single", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void GetHue() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "GetHue", 0, Array.Empty<string>(), "System.Single");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "GetSaturation", 0, ReturnTypeName = "System.Single", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void GetSaturation() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "GetSaturation", 0, Array.Empty<string>(), "System.Single");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "ToArgb", 0, ReturnTypeName = "System.Int32", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void ToArgb() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "ToArgb", 0, Array.Empty<string>(), "System.Int32");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "ToString", 0, ReturnTypeName = "System.String", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void ToString() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "ToString", 0, Array.Empty<string>(), "System.String");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "op_Equality", 0, "ConnectorLib.JSON.ParameterColorValue", "ConnectorLib.JSON.ParameterColorValue", ReturnTypeName = "System.Boolean", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void op_Equality() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "op_Equality", 0, new[] { "ConnectorLib.JSON.ParameterColorValue", "ConnectorLib.JSON.ParameterColorValue" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "op_Inequality", 0, "ConnectorLib.JSON.ParameterColorValue", "ConnectorLib.JSON.ParameterColorValue", ReturnTypeName = "System.Boolean", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void op_Inequality() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "op_Inequality", 0, new[] { "ConnectorLib.JSON.ParameterColorValue", "ConnectorLib.JSON.ParameterColorValue" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "Equals", 0, "System.Object", ReturnTypeName = "System.Boolean", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void Equals_System_Object() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "Equals", 0, new[] { "System.Object" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "Equals", 0, "ConnectorLib.JSON.ParameterColorValue", ReturnTypeName = "System.Boolean", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void Equals_ConnectorLib_JSON_ParameterColorValue() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "Equals", 0, new[] { "ConnectorLib.JSON.ParameterColorValue" }, "System.Boolean");

    [MethodBehaviorTest("ConnectorLib.JSON.ParameterColorValue", "GetHashCode", 0, ReturnTypeName = "System.Int32", SourcePath = @"ParameterColorValue.cs")]
    [TestMethod]
    public void GetHashCode() =>
        MethodBehaviorAssertions.AssertMethodBehavior(typeof(ConnectorLib.JSON.EffectResponse).Assembly, "ConnectorLib.JSON.ParameterColorValue", "GetHashCode", 0, Array.Empty<string>(), "System.Int32");
}
