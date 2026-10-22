using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using ConnectorLib.JSON;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConnectorLib.JSON.Tests;

[TestClass]
public class PublicApiSurfaceGuardTests
{
    private const string ExpectedApiFingerprint = "0715DA6E419D3A1BC79810191901248338F784D146FC28E5674C399F41EB3727";

    [TestMethod]
    public void TestFilesMirrorEveryProductionSourceAndContainMSTestCoverage()
    {
        string testRoot = GetTestRoot();
        string repository = Directory.GetParent(testRoot)!.FullName;
        string[] sourcePaths = GetProductionSourcePaths(repository);
        string[] testPaths = GetTestPaths(testRoot);

        CollectionAssert.AreEqual(sourcePaths, testPaths, "The test project must mirror production .cs paths exactly.");

        Assembly testAssembly = typeof(PublicApiSurfaceGuardTests).Assembly;
        foreach (string sourcePath in sourcePaths)
        {
            string fileName = Path.GetFileNameWithoutExtension(sourcePath);
            string testSourcePath = Path.Combine(testRoot, sourcePath.Replace('/', Path.DirectorySeparatorChar));
            string testSource = File.ReadAllText(testSourcePath);
            Assert.IsTrue(
                testSource.Contains($"class {fileName}Tests", StringComparison.Ordinal) &&
                testSource.Contains("[TestMethod]", StringComparison.Ordinal),
                $"The counterpart '{sourcePath}' must declare its matching test class and a test method in that file.");

            Type[] matchingTestClasses = testAssembly.GetTypes()
                .Where(type => type.Name == $"{fileName}Tests")
                .ToArray();
            Assert.IsTrue(
                matchingTestClasses.Any(type =>
                    type.GetCustomAttribute<TestClassAttribute>() != null &&
                    type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                        .Any(method => method.GetCustomAttribute<TestMethodAttribute>() != null)),
                $"The counterpart '{sourcePath}' must contain a matching MSTest class with at least one test method.");
        }
    }

    [TestMethod]
    public void EveryAuthoredProductionMethodHasOneBehaviorTest()
    {
        MethodInfo[] productionMethods = GetAuthoredProductionMethods();
        MethodInfo[] testMethods = typeof(PublicApiSurfaceGuardTests).Assembly.GetTypes()
            .Where(type => type != typeof(PublicApiSurfaceGuardTests))
            .SelectMany(type => type.GetMethods(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            .Where(method =>
                method.GetCustomAttribute<TestMethodAttribute>() != null &&
                method.GetCustomAttribute<MethodBehaviorTestAttribute>() != null)
            .ToArray();

        string testRoot = GetTestRoot();
        Dictionary<MethodInfo, MethodInfo> mappedProductionMethods = new();
        Dictionary<Type, string[]> declaringTypeSourcePaths = new();
        List<string> invalidMappings = new();

        foreach (MethodInfo testMethod in testMethods)
        {
            if (testMethod.DeclaringType!.GetCustomAttribute<TestClassAttribute>() == null)
            {
                invalidMappings.Add(
                    $"{testMethod.DeclaringType!.FullName}.{testMethod.Name} is not in an MSTest test class");
            }

            MethodBehaviorTestAttribute[] mappings =
                testMethod.GetCustomAttributes<MethodBehaviorTestAttribute>().ToArray();
            if (mappings.Length != 1)
            {
                invalidMappings.Add(
                    $"{testMethod.DeclaringType!.FullName}.{testMethod.Name} must map to exactly one production method");
                continue;
            }

            MethodBehaviorTestAttribute mapping = mappings[0];
            string testSourcePath = NormalizeRelativePath(mapping.SourcePath);
            string testSourceFile = Path.Combine(
                testRoot,
                testSourcePath.Replace('/', Path.DirectorySeparatorChar));
            if (string.IsNullOrWhiteSpace(mapping.SourcePath) ||
                !File.Exists(testSourceFile) ||
                !File.ReadAllText(testSourceFile).Contains($"{testMethod.Name}(", StringComparison.Ordinal))
            {
                invalidMappings.Add(
                    $"{testMethod.DeclaringType!.FullName}.{testMethod.Name} must be declared in its mirrored source test file");
                continue;
            }

            MethodInfo[] matchingProductionMethods = productionMethods.Where(method =>
                string.Equals(method.DeclaringType!.FullName, mapping.DeclaringTypeName, StringComparison.Ordinal) &&
                method.Name == mapping.MethodName &&
                method.GetGenericArguments().Length == mapping.GenericArity &&
                method.GetParameters().Select(parameter => parameter.ParameterType.ToString())
                    .SequenceEqual(mapping.ParameterTypeNames, StringComparer.Ordinal) &&
                (string.IsNullOrEmpty(mapping.ReturnTypeName) ||
                 string.Equals(method.ReturnType.ToString(), mapping.ReturnTypeName, StringComparison.Ordinal)))
                .ToArray();

            if (matchingProductionMethods.Length != 1)
            {
                invalidMappings.Add(
                    $"{testMethod.DeclaringType!.FullName}.{testMethod.Name} maps to {matchingProductionMethods.Length} production methods; " +
                    "include a declaring type and return type when needed to disambiguate");
                continue;
            }

            MethodInfo productionMethod = matchingProductionMethods[0];
            Type declaringType = productionMethod.DeclaringType!;
            if (!declaringTypeSourcePaths.TryGetValue(declaringType, out string[]? sourcePaths))
            {
                sourcePaths = GetTypeSourcePaths(declaringType, Directory.GetParent(testRoot)!.FullName);
                declaringTypeSourcePaths.Add(declaringType, sourcePaths);
            }

            if (!sourcePaths.Contains(testSourcePath, StringComparer.Ordinal))
            {
                invalidMappings.Add(
                    $"{testMethod.DeclaringType!.FullName}.{testMethod.Name} is not in the mirrored source file for " +
                    $"{declaringType.FullName}; expected one of: {string.Join(", ", sourcePaths)}");
                continue;
            }

            if (!mappedProductionMethods.TryAdd(productionMethod, testMethod))
            {
                invalidMappings.Add(
                    $"{productionMethod.DeclaringType!.FullName}.{FormatMethod(productionMethod)} has more than one test");
            }

            string methodName = GetSimpleMethodName(productionMethod);
            bool isOverloaded = productionMethods.Count(method =>
                method.DeclaringType == productionMethod.DeclaringType &&
                GetSimpleMethodName(method) == methodName) > 1;
            bool isExplicitInterfaceImplementation =
                productionMethod.Name.LastIndexOf('.') >= 0;
            bool hasReturnOnlyOverload = productionMethods.Any(method =>
                method != productionMethod &&
                method.DeclaringType == productionMethod.DeclaringType &&
                method.Name == productionMethod.Name &&
                method.GetGenericArguments().Length == productionMethod.GetGenericArguments().Length &&
                method.GetParameters().Select(parameter => parameter.ParameterType)
                    .SequenceEqual(productionMethod.GetParameters().Select(parameter => parameter.ParameterType)));
            string signatureSuffix = GetSignatureSuffix(productionMethod);
            if (hasReturnOnlyOverload)
            {
                signatureSuffix += $"_Returns_{Sanitize(productionMethod.ReturnType.ToString())}";
            }
            string expectedTestName = isOverloaded || isExplicitInterfaceImplementation
                ? $"{methodName}_{signatureSuffix}"
                : methodName;
            if (testMethod.Name != expectedTestName)
            {
                invalidMappings.Add(
                    $"{testMethod.DeclaringType!.FullName}.{testMethod.Name} must be named {expectedTestName}");
            }
        }

        string[] uncoveredMethods = productionMethods
            .Where(method => !mappedProductionMethods.ContainsKey(method))
            .Select(method => $"{method.DeclaringType!.FullName}.{FormatMethod(method)}")
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        Assert.IsTrue(
            uncoveredMethods.Length == 0 && invalidMappings.Count == 0,
            $"Authored production methods: {productionMethods.Length}; mapped behavior tests: {mappedProductionMethods.Count}. " +
            $"Uncovered methods ({uncoveredMethods.Length}): {string.Join("; ", uncoveredMethods)}. " +
            $"Invalid mappings or names ({invalidMappings.Count}): {string.Join("; ", invalidMappings)}.");
    }

    [TestMethod]
    public void PublicApiChangesRequireAnExplicitTestUpdate()
    {
        Type[] publicTypes = typeof(EffectResponse).Assembly.GetExportedTypes()
            .OrderBy(type => type.FullName, StringComparer.Ordinal)
            .ToArray();
        string[] apiEntries = publicTypes
            .SelectMany(type => type.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(member => member.MemberType is MemberTypes.Constructor or MemberTypes.Event or MemberTypes.Field or MemberTypes.Method or MemberTypes.Property)
                .Select(member => $"{type.FullName}|{member.MemberType}|{member}"))
            .OrderBy(entry => entry, StringComparer.Ordinal)
            .ToArray();
        string fingerprint = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(string.Join('\n', apiEntries))));

        Assert.AreEqual(
            ExpectedApiFingerprint,
            fingerprint,
            $"The ConnectorLib.JSON public API changed ({publicTypes.Length} types, {apiEntries.Length} members; fingerprint {fingerprint}). Add behavior tests for the change before updating this guard.");
    }

    private static MethodInfo[] GetAuthoredProductionMethods()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                  BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

        return typeof(EffectResponse).Assembly.GetTypes()
            .Where(type =>
                !type.Name.StartsWith("<", StringComparison.Ordinal) &&
                !type.FullName!.StartsWith("<", StringComparison.Ordinal) &&
                !typeof(MulticastDelegate).IsAssignableFrom(type) &&
                type.GetCustomAttribute<CompilerGeneratedAttribute>() == null)
            .SelectMany(type => type.GetMethods(flags))
            .Where(method =>
            {
                string metadataName = method.Name[(method.Name.LastIndexOf('.') + 1)..];
                bool isAccessor = method.IsSpecialName &&
                                 (metadataName.StartsWith("get_", StringComparison.Ordinal) ||
                                  metadataName.StartsWith("set_", StringComparison.Ordinal) ||
                                  metadataName.StartsWith("add_", StringComparison.Ordinal) ||
                                  metadataName.StartsWith("remove_", StringComparison.Ordinal) ||
                                  metadataName.StartsWith("raise_", StringComparison.Ordinal));

                return !isAccessor &&
                       !method.Name.StartsWith("<", StringComparison.Ordinal) &&
                       method.GetCustomAttribute<CompilerGeneratedAttribute>() == null;
            })
            .ToArray();
    }

    private static string FormatMethod(MethodInfo method) =>
        $"{method.Name}`{method.GetGenericArguments().Length}(" +
        string.Join(", ", method.GetParameters().Select(parameter => parameter.ParameterType)) + ")";

    private static string GetSimpleMethodName(MethodInfo method) =>
        method.Name[(method.Name.LastIndexOf('.') + 1)..];

    private static string GetSignatureSuffix(MethodInfo method)
    {
        string parameterSuffix = string.Join(
            "_",
            method.GetParameters().Select(parameter => Sanitize(parameter.ParameterType.ToString())));
        int interfaceSeparator = method.Name.LastIndexOf('.');
        if (interfaceSeparator >= 0)
        {
            string interfaceMethod = Sanitize(method.Name[..interfaceSeparator]);
            parameterSuffix = $"{interfaceMethod}_{parameterSuffix}";
        }
        if (method.GetGenericArguments().Length > 0)
        {
            parameterSuffix = $"Generic{method.GetGenericArguments().Length}_{parameterSuffix}";
        }

        return parameterSuffix.Length == 0 ? "NoParameters" : parameterSuffix;
    }

    private static string Sanitize(string value) =>
        new(value.Select(character => char.IsLetterOrDigit(character) ? character : '_').ToArray());

    private static string[] GetProductionSourcePaths(string repository)
    {
        (string Root, string Prefix)[] roots =
        {
            (repository, ""),
            (Path.Combine(repository, "Protocol.Shared"), "Protocol.Shared")
        };

        return roots.SelectMany(root => Directory.EnumerateFiles(root.Root, "*.cs", SearchOption.AllDirectories)
                .Where(path => !IsBuildArtifact(path))
                .Select(path => (root.Prefix, Relative: NormalizeRelativePath(Path.GetRelativePath(root.Root, path))))
                .Where(item => !IsExcludedSource(item.Prefix, item.Relative))
                .Select(item => item.Prefix.Length == 0 ? item.Relative : $"{item.Prefix}/{item.Relative}"))
            .OrderBy(path => path, StringComparer.Ordinal)
            .ToArray();
    }

    private static bool IsExcludedSource(string prefix, string relative) =>
        prefix.Length == 0 &&
        (relative.StartsWith("ConnectorLib.JSON.Tests/", StringComparison.Ordinal) ||
         relative.StartsWith("Protocol.Shared/", StringComparison.Ordinal));

    private static string GetTestRoot()
    {
        DirectoryInfo? repository = new(AppContext.BaseDirectory);
        while (repository != null &&
               (!Directory.Exists(Path.Combine(repository.FullName, "Protocol.Shared")) ||
                !Directory.Exists(Path.Combine(repository.FullName, "ConnectorLib.JSON.Tests"))))
        {
            repository = repository.Parent;
        }

        Assert.IsNotNull(repository, "Could not locate the ConnectorLib.JSON test project.");
        return Path.Combine(repository!.FullName, "ConnectorLib.JSON.Tests");
    }

    private static string[] GetTypeSourcePaths(Type type, string sourceRoot)
    {
        string typeName = type.Name;
        int genericArityStart = typeName.IndexOf('`');
        if (genericArityStart >= 0)
            typeName = typeName[..genericArityStart];

        Regex declaration = new(
            $@"\b(?:class|struct|interface|enum|delegate|record(?:\s+(?:class|struct))?)\s+{Regex.Escape(typeName)}\b",
            RegexOptions.CultureInvariant);
        Regex delegateDeclaration = new(
            $@"\bdelegate\s+[^;()]*\b{Regex.Escape(typeName)}\s*(?:<|\()",
            RegexOptions.CultureInvariant);
        (string Root, string Prefix)[] roots =
        {
            (sourceRoot, ""),
            (Path.Combine(sourceRoot, "Protocol.Shared"), "Protocol.Shared")
        };
        return roots.SelectMany(root => Directory.EnumerateFiles(root.Root, "*.cs", SearchOption.AllDirectories)
            .Where(path =>
            {
                if (IsBuildArtifact(path))
                    return false;

                string relative = NormalizeRelativePath(Path.GetRelativePath(root.Root, path));
                if (IsExcludedSource(root.Prefix, relative))
                    return false;
                string source = File.ReadAllText(path);
                return declaration.IsMatch(source) || delegateDeclaration.IsMatch(source);
            })
            .Select(path =>
            {
                string relative = NormalizeRelativePath(Path.GetRelativePath(root.Root, path));
                return root.Prefix.Length == 0 ? relative : $"{root.Prefix}/{relative}";
            }))
            .Distinct(StringComparer.Ordinal)
            .OrderBy(path => path, StringComparer.Ordinal)
            .ToArray();
    }

    private static string[] GetTestPaths(string root) =>
        Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories)
            .Where(path => !IsBuildArtifact(path))
            .Select(path => NormalizeRelativePath(Path.GetRelativePath(root, path)))
            .Where(path => path is not "MSTestSettings.cs" and not "PublicApiSurfaceGuardTests.cs")
            .OrderBy(path => path, StringComparer.Ordinal)
            .ToArray();

    private static bool IsBuildArtifact(string path) =>
        path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            .Any(segment => segment is "bin" or "obj");

    private static string NormalizeRelativePath(string path) =>
        path.Replace(Path.DirectorySeparatorChar, '/').Replace(Path.AltDirectorySeparatorChar, '/');
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
internal sealed class MethodBehaviorTestAttribute : Attribute
{
    public MethodBehaviorTestAttribute(
        Type declaringType,
        string methodName,
        int genericArity,
        params string[] parameterTypeNames)
        : this(declaringType.FullName ?? declaringType.Name, methodName, genericArity, parameterTypeNames)
    {
    }

    public MethodBehaviorTestAttribute(
        string declaringTypeName,
        string methodName,
        int genericArity,
        params string[] parameterTypeNames)
    {
        DeclaringTypeName = declaringTypeName;
        MethodName = methodName;
        GenericArity = genericArity;
        ParameterTypeNames = parameterTypeNames;
    }

    public string DeclaringTypeName { get; set; }
    public string MethodName { get; }
    public int GenericArity { get; }
    public string[] ParameterTypeNames { get; }
    public string ReturnTypeName { get; set; } = string.Empty;
    public string SourcePath { get; set; } = string.Empty;
}

internal static class MethodBehaviorAssertions
{
    private static readonly ConcurrentDictionary<JsonTextWriter, StringWriter> JsonOutput = new();

    public static void AssertMethodBehavior(Assembly assembly, string typeName, string methodName, int genericArity, string[] parameterTypes, string returnType)
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
        Type? openType = assembly.GetType(typeName);
        Assert.IsNotNull(openType, $"Missing production type {typeName}.");
        MethodInfo[] methods = openType!.GetMethods(flags).Where(method =>
            method.Name == methodName &&
            method.GetGenericArguments().Length == genericArity &&
            method.GetParameters().Select(parameter => parameter.ParameterType.ToString())
                .SequenceEqual(parameterTypes, StringComparer.Ordinal) &&
            method.ReturnType.ToString() == returnType).ToArray();
        Assert.AreEqual(1, methods.Length, $"Could not resolve {typeName}.{methodName}.");

        Type closedType = CloseGenericType(openType);
        MethodInfo method = closedType.GetMethods(flags).Single(candidate => candidate.MetadataToken == methods[0].MetadataToken);
        if (method.IsGenericMethodDefinition)
            method = method.MakeGenericMethod(method.GetGenericArguments().Select(ChooseGenericType).ToArray());
        if (method.IsAbstract)
        {
            Assert.IsTrue(method.IsPublic || method.IsFamily || method.IsFamilyOrAssembly,
                $"{typeName}.{methodName} must remain a visible abstract contract.");
            Assert.IsNotNull(method.ReturnType);
            return;
        }
        object? target = method.IsStatic ? null : CreateTarget(method.DeclaringType!, assembly);
        object?[] arguments = method.GetParameters().Select(parameter =>
            CreateValue(parameter.ParameterType, parameter.Name ?? string.Empty, assembly, typeName, methodName, 0)).ToArray();
        SeedTarget(target, arguments, assembly, typeName, methodName);

        string simpleName = method.Name[(method.Name.LastIndexOf('.') + 1)..];
        if (simpleName == "Equals" && arguments.Length == 1 && target != null)
            arguments[0] = target;
        foreach (ParameterInfo parameter in method.GetParameters())
        {
            if (parameter.ParameterType.IsEnum)
            {
                string predicate = simpleName.StartsWith("Is", StringComparison.Ordinal) ? simpleName[2..] : simpleName;
                string? matchingName = Enum.GetNames(parameter.ParameterType)
                    .FirstOrDefault(value => value.Contains(predicate, StringComparison.OrdinalIgnoreCase));
                if (matchingName != null)
                    arguments[parameter.Position] = Enum.Parse(parameter.ParameterType, matchingName);
            }
        }

        object? result;
        try
        {
            result = method.Invoke(target, arguments);
        }

        catch (TargetInvocationException exception) when (exception.InnerException != null)
        {
            if (typeName == "CrowdControl.Common.Converters.EffectRequestConverter" &&
                methodName == "WriteJson" && exception.InnerException is NotImplementedException ||
                typeName == "CrowdControl.Common.EffectResponse+MetadataConverter" &&
                methodName == "ReadJson" && exception.InnerException is NotImplementedException)
            {
                Assert.IsInstanceOfType<NotImplementedException>(exception.InnerException);
                return;
            }
            Assert.Fail($"{typeName}.{methodName} threw {exception.InnerException.GetType().Name}: {exception.InnerException.Message}");
            return;
        }

        AssertBehaviorResult(method, result, target, arguments);
    }

    public static void AssertSourceTypeBehavior(Assembly assembly, string[] typeNames)
    {
        Assert.IsTrue(typeNames.Length > 0, "Each mirrored source test must identify its production type.");
        foreach (string typeName in typeNames)
        {
            Type? type = assembly.GetType(typeName);
            Assert.IsNotNull(type, $"Missing production type {typeName}.");
            type = CloseGenericType(type!);
            if (type!.IsGenericType &&
                (type.Name.StartsWith("NumericRange", StringComparison.Ordinal) ||
                 type.Name.StartsWith("NonNumericRange", StringComparison.Ordinal)))
            {
                Type valueType = type.GetGenericArguments()[0];
                object range = Activator.CreateInstance(type, Convert.ChangeType(1, valueType), Convert.ChangeType(3, valueType))!;
                Assert.AreEqual(Convert.ChangeType(1, valueType), type.GetField("Minimum")!.GetValue(range));
                Assert.AreEqual(Convert.ChangeType(3, valueType), type.GetField("Maximum")!.GetValue(range));
            }
            else if (type.IsEnum)
            {
                Array values = Enum.GetValues(type);
                Assert.IsTrue(values.Length > 0, $"{type.FullName} must define values.");
                foreach (object value in values)
                    Assert.IsTrue(Enum.IsDefined(type, value));
            }
            else if (!type.IsInterface && !type.IsAbstract &&
                     (type.IsValueType || type.GetConstructor(Type.EmptyTypes) != null))
            {
                object instance = Activator.CreateInstance(type)!;
                string json = JsonConvert.SerializeObject(instance);
                Assert.IsNotNull(JToken.Parse(json), $"{type.FullName} did not serialize to valid JSON.");
            }
            else
            {
                Assert.IsTrue(type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).Length > 0,
                    $"{type.FullName} must expose its declared contract.");
            }
        }
    }

    private static Type CloseGenericType(Type type) =>
        type.ContainsGenericParameters
            ? type.MakeGenericType(type.GetGenericArguments().Select(ChooseGenericType).ToArray())
            : type;

    private static Type ChooseGenericType(Type parameter)
    {
        if (parameter.Name.Contains("TList", StringComparison.Ordinal))
        {
            Type? quickList = parameter.DeclaringType?.Assembly.GetType("CrowdControl.Common.QuickList`1");
            if (quickList != null)
                return quickList.MakeGenericType(typeof(string));
            return typeof(List<string>);
        }
        if (parameter.GetGenericParameterConstraints().Contains(typeof(Enum)))
        {
            Type? enumType = parameter.DeclaringType?.Assembly.GetTypes()
                .FirstOrDefault(type => type.IsEnum && type.FullName == "CrowdControl.Common.Orderliness");
            if (enumType != null)
                return enumType;
        }
        if ((parameter.GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0)
            return typeof(int);
        return typeof(string);
    }

    private static object CreateParameterDef(Type type)
    {
        Type parameterType = type.Assembly.GetType("CrowdControl.Common.ParameterBase+ParameterType")!;
        return Activator.CreateInstance(type, "Parameter", "parameter", Enum.Parse(parameterType, "HexColor"))!;
    }

    private static object? CreateTarget(Type type, Assembly assembly)
    {
        if (type.FullName == "ConnectorLib.JSON.SimpleJSONMessage")
            return CreateTarget(type.Assembly.GetType("ConnectorLib.JSON.SimpleJSONRequest")!, assembly);
        if (type.FullName == "ConnectorLib.JSON.RequestParameters")
        {
            Type parameterType = type.Assembly.GetType("ConnectorLib.JSON.ParameterValue`1")!.MakeGenericType(typeof(string));
            object parameter = Activator.CreateInstance(parameterType, "Parameter", "key", "sample")!;
            List<IParameterValue> parameters = new() { (IParameterValue)parameter };
            return Activator.CreateInstance(type, parameters);
        }
        if (type.FullName == "CrowdControl.Common.CrowdControlBlock")
            return CreateTarget(type.Assembly.GetType("CrowdControl.Common.EffectRequest")!, assembly);
        if (type.FullName == "CrowdControl.Common.TikTokSourceDetails")
            return Activator.CreateInstance(type.Assembly.GetType("CrowdControl.Common.TikTokGiftSourceDetails")!);
        if (type.FullName == "CrowdControl.Common.ResponseBlock")
        {
            Type responseType = type.Assembly.GetType("CrowdControl.Common.DataResponse")!;
            return CreateTarget(responseType, assembly);
        }
        if (type.FullName == "CrowdControl.Common.Effect")
            return Activator.CreateInstance(type, "Test effect", "effect_id");
        if (type.FullName == "CrowdControl.Common.EffectRequest")
        {
            Type effectType = type.Assembly.GetType("CrowdControl.Common.Effect")!;
            object effect = Activator.CreateInstance(effectType, "Test effect", "effect_id")!;
            return Activator.CreateInstance(type, effect);
        }
        if (type.FullName == "CrowdControl.Common.ParameterDef")
            return CreateParameterDef(type);
        if (type.FullName == "CrowdControl.Common.ParameterList")
        {
            Type parameterType = type.Assembly.GetType("CrowdControl.Common.ParameterDef")!;
            object parameter = CreateParameterDef(parameterType);
            Array values = Array.CreateInstance(parameterType, 1);
            values.SetValue(parameter, 0);
            return Activator.CreateInstance(type, values);
        }
        if (type.FullName == "CrowdControl.Common.ParameterResults")
        {
            Type valueType = type.Assembly.GetType("CrowdControl.Common.ParameterValue`1")!.MakeGenericType(typeof(string));
            object value = Activator.CreateInstance(valueType, "Value", "value", "sample")!;
            Array values = Array.CreateInstance(valueType, 1);
            values.SetValue(value, 0);
            return Activator.CreateInstance(type, values);
        }
        if (type.FullName == "CrowdControl.Common.HypeTrainSourceDetails")
        {
            object instance = Activator.CreateInstance(type)!;
            Type contributionType = type.GetNestedType("Contribution")!;
            object contribution = Activator.CreateInstance(contributionType)!;
            IList contributions = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(contributionType))!;
            contributions.Add(contribution);
            type.GetProperty("TopContributions")!.SetValue(instance, contributions);
            type.GetProperty("LastContribution")!.SetValue(instance, contribution);
            return instance;
        }
        if (type.FullName == "CrowdControl.Common.IEffectPackProcessable")
        {
            Type effectType = type.Assembly.GetType("CrowdControl.Common.Effect")!;
            object effect = Activator.CreateInstance(effectType, "Test effect", "effect_id")!;
            return Activator.CreateInstance(type.Assembly.GetType("CrowdControl.Common.EffectRequest")!, effect);
        }
        if (type.IsInterface || type.IsAbstract)
        {
            string? implementation = type.FullName switch
            {
                "CrowdControl.Common.IEffectPackProcessable" => "CrowdControl.Common.EffectRequest",
                "CrowdControl.Common.IEffectSourceDetails" => "CrowdControl.Common.TwitchChannelRewardSourceDetails",
                "ConnectorLib.JSON.SimpleJSONMessage" => "ConnectorLib.JSON.SimpleJSONRequest",
                _ => null
            };
            if (implementation == null)
                return null;
            type = type.Assembly.GetType(implementation)!;
        }
        foreach (ConstructorInfo constructor in type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                     .OrderBy(candidate => candidate.GetParameters().Length))
        {
            try
            {
                object?[] arguments = constructor.GetParameters()
                    .Select(parameter => CreateValue(parameter.ParameterType, parameter.Name ?? string.Empty, assembly,
                        type.FullName ?? type.Name, constructor.Name, 0)).ToArray();
                return constructor.Invoke(arguments);
            }
            catch
            {
            }
        }
        try { return Activator.CreateInstance(type, nonPublic: true); }
        catch { return RuntimeHelpers.GetUninitializedObject(type); }
    }

    private static void SeedTarget(object? target, object?[] arguments, Assembly assembly, string typeName, string methodName)
    {
        if (target == null)
            return;
        if (methodName is "Add" or "TryAdd")
            return;
        Type type = target.GetType();
        MethodInfo[] addMethods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(method => method.Name == "Add" && !method.IsGenericMethodDefinition)
            .OrderBy(method => method.GetParameters().Length)
            .ToArray();
        foreach (MethodInfo add in addMethods)
        {
            try
            {
                object?[] values = add.GetParameters().Select(parameter =>
                {
                    return CreateValue(parameter.ParameterType, parameter.Name ?? string.Empty, assembly, typeName, methodName, 0);
                }).ToArray();
                for (int i = 0; i < values.Length; i++)
                {
                    Type parameterType = add.GetParameters()[i].ParameterType;
                    if (!parameterType.IsValueType)
                    {
                        object? matching = arguments.FirstOrDefault(value => value != null && parameterType.IsInstanceOfType(value));
                        if (matching != null)
                            values[i] = matching;
                    }
                }
                add.Invoke(target, values);
                return;
            }
            catch
            {
            }
        }
    }

    private static object? CreateValue(Type type, string name, Assembly assembly, string declaringTypeName, string methodName, int depth)
    {
        if (type.IsByRef)
            type = type.GetElementType()!;
        if (depth > 3)
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        if (type == typeof(string))
            return methodName == "LoadFrom" ? assembly.Location :
                methodName == "Load" ? assembly.GetName().Name :
                declaringTypeName == "ConnectorLib.JSON.SimpleJSONRequest" && methodName == "TryParse" ? "{\"type\":\"version\"}" :
                declaringTypeName == "ConnectorLib.JSON.SimpleJSONResponse" && methodName == "TryParse" ? "{\"type\":\"keepAlive\"}" :
                declaringTypeName == "ConnectorLib.JSON.StringEx" && methodName == "ToCamelCase" ? "BehaviorTest" :
                declaringTypeName == "ConnectorLib.JSON.HexColorConverter" && methodName == "TryParse" ? "#112233" :
                declaringTypeName.Contains("CrowdControlBlock", StringComparison.Ordinal) && methodName == "Deserialize"
                    ? "{\"type\":16,\"requestID\":\"00112233-4455-6677-8899-aabbccddeeff\",\"effect\":{\"id\":\"effect_id\",\"name\":\"Test\"}}"
                    :
                name.Contains("path", StringComparison.OrdinalIgnoreCase) ? Path.GetDirectoryName(assembly.Location) :
                name.Contains("assembly", StringComparison.OrdinalIgnoreCase) ? assembly.GetName().Name :
                declaringTypeName.Contains("SessionID", StringComparison.Ordinal) ? "01ARZ3NDEKTSV4RRFFQ69G5FAV" :
                declaringTypeName.Contains("VersionNumber", StringComparison.Ordinal) ||
                name.Contains("version", StringComparison.OrdinalIgnoreCase) ? "1.2.3" :
                name.Contains("culture", StringComparison.OrdinalIgnoreCase) ? "en-US" :
                name.Equals("key", StringComparison.OrdinalIgnoreCase) ? "key" : "behavior-test";
        if (type == typeof(Enum) && declaringTypeName == "ConnectorLib.JSON.EnumEx")
            return Enum.Parse(assembly.GetType("ConnectorLib.JSON.RequestType")!, "EffectStart");
        if (type == typeof(bool)) return true;
        if (type == typeof(byte)) return (byte)17;
        if (type == typeof(short)) return (short)2;
        if (type == typeof(ushort)) return (ushort)2;
        if (type == typeof(int)) return 2;
        if (type == typeof(uint)) return 2u;
        if (type == typeof(long)) return 2L;
        if (type == typeof(ulong)) return 2UL;
        if (type == typeof(float)) return 1.5f;
        if (type == typeof(double)) return 1.5d;
        if (type == typeof(decimal)) return 1.5m;
        if (type == typeof(char)) return 'x';
        if (type == typeof(Guid)) return Guid.Parse("00112233-4455-6677-8899-aabbccddeeff");
        if (type == typeof(SerializationInfo))
            return new SerializationInfo(typeof(object), new FormatterConverter());
        if (type == typeof(TimeSpan)) return TimeSpan.FromSeconds(2);
        if (type == typeof(DateTime)) return new DateTime(2020, 1, 2, 3, 4, 5, DateTimeKind.Utc);
        if (type == typeof(CultureInfo)) return CultureInfo.GetCultureInfo("en-US");
        if (type.FullName is "ConnectorLib.JSON.RequestParameters" or "CrowdControl.Common.Effect" or "CrowdControl.Common.EffectRequest" or
            "CrowdControl.Common.ParameterDef" or "CrowdControl.Common.ParameterList" or "CrowdControl.Common.ParameterResults" or
            "CrowdControl.Common.HypeTrainSourceDetails" or "CrowdControl.Common.ResponseBlock" or
            "CrowdControl.Common.CrowdControlBlock")
            return CreateTarget(type, assembly);
        if (type == typeof(Type))
        {
            if (declaringTypeName.Contains("CamelCaseStringEnumConverter", StringComparison.Ordinal))
                return assembly.GetType("ConnectorLib.JSON.RequestType")!;
            string sourceTypeName = declaringTypeName.Split('+')[0];
            if (methodName == "GetBlockType")
                return assembly.GetType("CrowdControl.Common.EffectRequest")!;
            if (sourceTypeName.EndsWith("VersionNumber", StringComparison.Ordinal))
                return assembly.GetType(sourceTypeName)!;
            if (declaringTypeName.Contains("NonNumericRangeConverter", StringComparison.Ordinal))
                return assembly.GetType("CrowdControl.Common.NonNumericRange`1")!.MakeGenericType(typeof(long));
            if (declaringTypeName.Contains("NumericRangeConverter", StringComparison.Ordinal))
                return assembly.GetType("CrowdControl.Common.NumericRange`1")!.MakeGenericType(typeof(long));
            if (declaringTypeName.Contains("ParameterDef", StringComparison.Ordinal))
                return assembly.GetType("CrowdControl.Common.ParameterDef")!;
            if (declaringTypeName.Contains("ParameterList", StringComparison.Ordinal))
                return assembly.GetType("CrowdControl.Common.ParameterList")!;
            if (declaringTypeName.Contains("ParameterResults", StringComparison.Ordinal))
                return assembly.GetType("CrowdControl.Common.ParameterResults")!;
            if (declaringTypeName.Contains("AlignmentValueConverter", StringComparison.Ordinal))
                return assembly.GetType("CrowdControl.Common.AlignmentValue`1")!.MakeGenericType(
                    assembly.GetType("CrowdControl.Common.Orderliness")!);
            if (declaringTypeName.Contains("QuickList", StringComparison.Ordinal))
                return assembly.GetType("CrowdControl.Common.QuickList`1")!.MakeGenericType(typeof(string));
            if (declaringTypeName.Contains("EffectRequestConverter", StringComparison.Ordinal))
                return assembly.GetType("CrowdControl.Common.EffectRequest")!;
            if (declaringTypeName.Contains("HexColor", StringComparison.Ordinal))
                return typeof(Color);
            if (declaringTypeName.Contains("CamelCaseStringEnumConverter", StringComparison.Ordinal))
                return assembly.GetTypes().First(type => type.IsEnum);
            return assembly.GetType(sourceTypeName) ?? typeof(object);
        }
        if (type == typeof(Assembly)) return assembly;
        if (type == typeof(Delegate)) return (Action)(() => { });
        if (type == typeof(object) && declaringTypeName.Contains("AlignmentValueConverter", StringComparison.Ordinal))
        {
            Type alignmentValue = assembly.GetType("CrowdControl.Common.AlignmentValue`1")!
                .MakeGenericType(assembly.GetType("CrowdControl.Common.Orderliness")!);
            return Activator.CreateInstance(alignmentValue, 0.5f);
        }
        if (typeof(MethodBase).IsAssignableFrom(type))
            return assembly.GetTypes()
                .Where(type => !type.IsInterface && !type.IsAbstract)
                .SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                .FirstOrDefault();
        if (type == typeof(Color)) return Color.FromArgb(0x11, 0x22, 0x33);
        if (type == typeof(StringBuilder)) return new StringBuilder("behavior-test");
        if (type == typeof(TextWriter)) return new StringWriter(CultureInfo.InvariantCulture);
        if (typeof(Stream).IsAssignableFrom(type)) return new MemoryStream();
        if (type == typeof(JsonSerializer)) return JsonSerializer.CreateDefault();
        if (typeof(JsonWriter).IsAssignableFrom(type))
        {
            StringWriter output = new(CultureInfo.InvariantCulture);
            JsonTextWriter writer = new(output);
            JsonOutput[writer] = output;
            return writer;
        }
        if (typeof(JsonReader).IsAssignableFrom(type))
        {
            string json = declaringTypeName == "ConnectorLib.JSON.CamelCaseStringEnumConverter" ? "\"effectStart\"" :
                declaringTypeName == "ConnectorLib.JSON.ParameterBase+ParameterTypeConverter" ? "\"hex-color\"" :
                declaringTypeName == "ConnectorLib.JSON.RequestParameters+Converter" ? "{\"key\":{\"title\":\"Parameter\",\"type\":\"options\",\"value\":\"sample\"}}" :
                declaringTypeName.Contains("EffectRequestConverter", StringComparison.Ordinal)
                ? "{\"effect\":{\"id\":\"effect_id\",\"name\":\"Test\"},\"requestID\":\"00112233-4455-6677-8899-aabbccddeeff\"}"
                : declaringTypeName.Contains("IEffectSourceDetails", StringComparison.Ordinal)
                    ? "{\"type\":\"twitch-channel-reward\",\"name\":\"Viewer\",\"rewardID\":\"reward\",\"redemptionID\":\"redemption\",\"twitchID\":\"123\"}"
                : declaringTypeName.Contains("MetadataConverter", StringComparison.Ordinal) ? "{}" :
                  declaringTypeName.Contains("AlignmentValueConverter", StringComparison.Ordinal) ? "0.5" :
                  declaringTypeName.Contains("Alignment", StringComparison.Ordinal) ? "{\"orderliness\":0.5,\"morality\":0.25}" :
                  declaringTypeName.Contains("NumericRangeConverter", StringComparison.Ordinal) ||
                  declaringTypeName.Contains("NonNumericRangeConverter", StringComparison.Ordinal) ||
                  declaringTypeName.Contains("QuickList", StringComparison.Ordinal) ? "[1,3]" :
                  declaringTypeName.Contains("ParameterList", StringComparison.Ordinal) ? "{\"parameter\":{\"id\":\"parameter\",\"name\":\"Parameter\",\"type\":\"HexColor\"}}" :
                  declaringTypeName.Contains("ParameterResults", StringComparison.Ordinal) ? "{\"value\":{\"id\":\"value\",\"name\":\"Value\",\"type\":\"Options\",\"value\":\"sample\"}}" :
                  declaringTypeName.Contains("EffectList", StringComparison.Ordinal) ? "[{\"id\":\"effect_id\",\"name\":\"Test\"}]" :
                  declaringTypeName.Contains("CrowdControlBlock", StringComparison.Ordinal) ? "{\"type\":16,\"requestID\":\"00112233-4455-6677-8899-aabbccddeeff\",\"effect\":{\"id\":\"effect_id\",\"name\":\"Test\"}}" :
                  declaringTypeName.Contains("ParameterDef", StringComparison.Ordinal) ? "{\"id\":\"p\",\"name\":\"Parameter\",\"type\":\"Options\"}" :
                  declaringTypeName.Contains("Effect", StringComparison.Ordinal) ? "{\"id\":\"effect_id\",\"name\":\"Test\"}" :
                  declaringTypeName.Contains("LocalizableString", StringComparison.Ordinal) ? "\"behavior-test\"" :
                  declaringTypeName.Contains("SessionID", StringComparison.Ordinal) ? "\"01ARZ3NDEKTSV4RRFFQ69G5FAV\"" :
                  declaringTypeName.Contains("HexColor", StringComparison.Ordinal) ? "\"#112233\"" :
                  declaringTypeName.Contains("VersionNumber", StringComparison.Ordinal) ? "\"1.2.3\"" : "1";
            JsonTextReader reader = new(new StringReader(json));
            reader.Read();
            return reader;
        }
        if (typeof(JToken).IsAssignableFrom(type))
        {
            string json =
                declaringTypeName == "ConnectorLib.JSON.CamelCaseStringEnumConverter" ? "\"effectStart\"" :
                declaringTypeName == "ConnectorLib.JSON.SimpleJSONRequest" ? "{\"type\":\"version\"}" :
                declaringTypeName == "ConnectorLib.JSON.SimpleJSONResponse" ? "{\"type\":\"keepAlive\"}" :
                declaringTypeName == "ConnectorLib.JSON.RequestParameters+Converter" ? "{\"key\":{\"title\":\"Parameter\",\"type\":\"options\",\"value\":\"sample\"}}" :
                declaringTypeName.Contains("CrowdControlBlock", StringComparison.Ordinal)
                    ? "{\"type\":16,\"requestID\":\"00112233-4455-6677-8899-aabbccddeeff\",\"effect\":{\"id\":\"effect_id\",\"name\":\"Test\"}}"
                : declaringTypeName.Contains("AlignmentValueConverter", StringComparison.Ordinal) ? "0.5"
                : declaringTypeName.Contains("Alignment", StringComparison.Ordinal) ? "{\"orderliness\":0.5,\"morality\":0.25}"
                : declaringTypeName.Contains("IEffectSourceDetails", StringComparison.Ordinal)
                    ? "{\"type\":\"twitch-channel-reward\",\"name\":\"Viewer\",\"rewardID\":\"reward\",\"redemptionID\":\"redemption\",\"twitchID\":\"123\"}"
                : declaringTypeName.Contains("ParameterList", StringComparison.Ordinal) ? "{\"parameter\":{\"id\":\"parameter\",\"name\":\"Parameter\",\"type\":\"HexColor\"}}"
                : declaringTypeName.Contains("ParameterResults", StringComparison.Ordinal) ? "{\"value\":{\"id\":\"value\",\"name\":\"Value\",\"type\":\"Options\",\"value\":\"sample\"}}"
                : declaringTypeName.Contains("EffectList", StringComparison.Ordinal) ? "[{\"id\":\"effect_id\",\"name\":\"Test\"}]"
                : declaringTypeName.Contains("ParameterDef", StringComparison.Ordinal) ? "{\"name\":\"Option\",\"id\":\"option\",\"type\":\"Options\"}"
                : methodName == "ReadOption" ? "{\"name\":\"Option\"}"
                : declaringTypeName.Contains("HexColor", StringComparison.Ordinal) ? "\"#112233\""
                : "{\"Name\":\"behavior-test\",\"Value\":2,\"Id\":2}";
            return JToken.Parse(json);
        }
        if (type.IsEnum)
        {
            if (declaringTypeName == "ConnectorLib.JSON.EnumEx")
                return Enum.Parse(assembly.GetType("ConnectorLib.JSON.RequestType")!, "EffectStart");
            if (type.FullName == "CrowdControl.Common.ParameterBase+ParameterType")
                return Enum.Parse(type, "HexColor");
            Array values = Enum.GetValues(type);
            return values.Length == 0 ? Activator.CreateInstance(type) : values.GetValue(0);
        }
        if (type.IsArray)
        {
            Type element = type.GetElementType()!;
            int length = methodName == "CopyTo" ? 8 : 1;
            Array values = Array.CreateInstance(element, length);
            for (int i = 0; i < values.Length; i++)
                values.SetValue(CreateValue(element, "value", assembly, declaringTypeName, methodName, depth + 1), i);
            return values;
        }
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            return CreateValue(Nullable.GetUnderlyingType(type)!, name, assembly, declaringTypeName, methodName, depth + 1);
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
        {
            Type[] args = type.GetGenericArguments();
            return Activator.CreateInstance(type,
                CreateValue(args[0], "key", assembly, declaringTypeName, methodName, depth + 1),
                CreateValue(args[1], "value", assembly, declaringTypeName, methodName, depth + 1));
        }
        if (typeof(Delegate).IsAssignableFrom(type))
        {
            if (type == typeof(Delegate))
                return (Action)(() => { });
            MethodInfo invoke = type.GetMethod("Invoke")!;
            ParameterExpression[] parameters = invoke.GetParameters()
                .Select(parameter => Expression.Parameter(parameter.ParameterType, parameter.Name)).ToArray();
            Expression body = invoke.ReturnType == typeof(void) ? Expression.Empty() :
                invoke.ReturnType == typeof(bool) ? Expression.Constant(true) :
                invoke.ReturnType == typeof(string) ? Expression.Constant("behavior-test") :
                invoke.ReturnType.IsGenericType && invoke.ReturnType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                    ? Expression.NewArrayInit(invoke.ReturnType.GetGenericArguments()[0],
                        Expression.Constant(CreateValue(invoke.ReturnType.GetGenericArguments()[0], "value", assembly, declaringTypeName, methodName, depth + 1),
                            invoke.ReturnType.GetGenericArguments()[0]))
                    : Expression.Default(invoke.ReturnType);
            return Expression.Lambda(type, body, parameters).Compile();
        }
        if (type.IsGenericType && type.GetGenericTypeDefinition() is var generic &&
            (generic == typeof(IEnumerable<>) || generic == typeof(ICollection<>) || generic == typeof(IList<>) ||
             generic == typeof(IReadOnlyCollection<>) || generic == typeof(IReadOnlyList<>)))
        {
            Type element = type.GetGenericArguments()[0];
            IList values = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(element))!;
            values.Add(CreateValue(element, "value", assembly, declaringTypeName, methodName, depth + 1));
            return values;
        }
        if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IDictionary<,>) ||
                                   type.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>)))
        {
            Type[] arguments = type.GetGenericArguments();
            Type dictionaryType = typeof(Dictionary<,>).MakeGenericType(arguments);
            IDictionary values = (IDictionary)Activator.CreateInstance(dictionaryType)!;
            values.Add(CreateValue(arguments[0], "key", assembly, declaringTypeName, methodName, depth + 1)!,
                CreateValue(arguments[1], "value", assembly, declaringTypeName, methodName, depth + 1));
            return values;
        }
        if (type.IsInterface || type.IsAbstract)
        {
            if (type.FullName == "CrowdControl.Common.IAlignmentValue")
            {
                Type alignmentValue = type.Assembly.GetType("CrowdControl.Common.AlignmentValue`1")!
                    .MakeGenericType(type.Assembly.GetType("CrowdControl.Common.Orderliness")!);
                return Activator.CreateInstance(alignmentValue, 0.5f);
            }
            if (type.Name == "IParameterValue")
            {
                Type? valueType = type.Assembly.GetType(type.Namespace + ".ParameterValue`1");
                if (valueType != null)
                    return CreateTarget(valueType.MakeGenericType(typeof(string)), assembly);
            }
            if (type.FullName == "Org.BouncyCastle.Crypto.AsymmetricKeyParameter")
            {
                Type? keyType = Type.GetType("Org.BouncyCastle.Crypto.Parameters.Ed25519PrivateKeyParameters, BouncyCastle.Cryptography");
                if (keyType != null)
                {
                    ConstructorInfo? constructor = keyType.GetConstructor(new[] { typeof(byte[]), typeof(int) });
                    if (constructor != null)
                        return constructor.Invoke(new object[] { new byte[32], 0 });
                }
            }
            string? implementation = type.FullName switch
            {
                "CrowdControl.Common.IEffectSourceDetails" => "CrowdControl.Common.TwitchChannelRewardSourceDetails",
                _ => null
            };
            return implementation == null ? null : CreateTarget(type.Assembly.GetType(implementation)!, assembly);
        }
        foreach (ConstructorInfo constructor in type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                     .OrderBy(candidate => candidate.GetParameters().Length))
        {
            try
            {
                object?[] arguments = constructor.GetParameters()
                    .Select(parameter => CreateValue(parameter.ParameterType, parameter.Name ?? string.Empty, assembly,
                        declaringTypeName, methodName, depth + 1)).ToArray();
                return constructor.Invoke(arguments);
            }
            catch
            {
            }
        }
        try { return Activator.CreateInstance(type, nonPublic: true); }
        catch { return type.IsValueType ? Activator.CreateInstance(type) : null; }
    }

    private static void AssertBehaviorResult(MethodInfo method, object? result, object? target, object?[] arguments)
    {
        string name = method.Name[(method.Name.LastIndexOf('.') + 1)..];
        if (name == "Equals" && method.ReturnType == typeof(bool))
        {
            Assert.AreEqual(true, result, $"{method.DeclaringType!.FullName} should recognize the equivalent value.");
            return;
        }
        if (name == "GetHashCode")
        {
            Assert.AreEqual(target!.GetHashCode(), result, "Equal calls must produce a stable hash code.");
            return;
        }
        if (name == "TryParse" && method.ReturnType == typeof(bool))
        {
            Assert.AreEqual(true, result, $"{method.DeclaringType!.FullName}.TryParse should accept the representative valid JSON input.");
            foreach (ParameterInfo parameter in method.GetParameters().Where(parameter => parameter.ParameterType.IsByRef))
            {
                Assert.IsNotNull(arguments[parameter.Position], "TryParse should populate its out value for valid input.");
                Assert.IsTrue(parameter.ParameterType.GetElementType()!.IsInstanceOfType(arguments[parameter.Position]));
            }
            return;
        }
        if (method.DeclaringType!.FullName == "ConnectorLib.JSON.RequestParameters")
        {
            if (name is "ContainsKey" or "TryGetValue" or "Any")
                Assert.AreEqual(true, result, $"RequestParameters.{name} should find its seeded representative value.");
            if (name.StartsWith("First", StringComparison.Ordinal))
                Assert.AreEqual("sample", result, $"RequestParameters.{name} should select its first seeded value.");
            if (result is IEnumerable values && result is not string)
                Assert.IsTrue(values.GetEnumerator().MoveNext(), $"RequestParameters.{name} should preserve the seeded value.");
        }
        if (method.DeclaringType!.FullName == "ConnectorLib.JSON.ParameterBase+ParameterTypeConverter" && name == "ReadJson")
            Assert.AreEqual(Enum.Parse(method.ReturnType, "HexColor"), result);
        if (method.DeclaringType!.FullName == "ConnectorLib.JSON.CamelCaseStringEnumConverter" && name is "ReadJson" or "ReadJToken")
        {
            Type enumType = (Type)arguments[1]!;
            Assert.AreEqual(Enum.Parse(enumType, "EffectStart"), result);
        }
        if (name == "ToString")
        {
            Assert.IsNotNull(result, "ToString must not return null.");
            Assert.IsInstanceOfType<string>(result);
            return;
        }
        if (name == "Clone")
        {
            Assert.IsNotNull(result);
            Assert.AreNotSame(target, result, "Clone must return an independent instance.");
            return;
        }
        if (name == "ToCamelCase")
        {
            if (method.DeclaringType!.FullName == "ConnectorLib.JSON.EnumEx")
            {
                string enumName = arguments[0]!.ToString()!;
                Assert.AreEqual(char.ToLowerInvariant(enumName[0]) + enumName[1..], result);
            }
            else
            {
                Assert.AreEqual("behaviorTest", result);
            }
            return;
        }
        if (name == "IsNullOrWhiteSpace")
        {
            Assert.AreEqual(false, result);
            return;
        }
        if (method.ReturnType == typeof(void))
        {
            Assert.AreEqual(method.IsStatic, target == null, $"{method.DeclaringType!.FullName}.{method.Name} target/static contract changed.");
            foreach (object? argument in arguments)
            {
                if (argument is JsonTextWriter jsonWriter)
                {
                    jsonWriter.Flush();
                    Assert.IsTrue(JsonOutput.TryGetValue(jsonWriter, out StringWriter? output) && output.GetStringBuilder().Length > 0,
                        $"{method.DeclaringType!.FullName}.{method.Name} must write JSON.");
                }
            }
            return;
        }
        if (result == null)
        {
            Assert.IsTrue(name.Contains("OrDefault", StringComparison.Ordinal) ||
                          name.Contains("GetAssembly", StringComparison.Ordinal) ||
                          name.Contains("GetPackVersion", StringComparison.Ordinal),
                $"{method.DeclaringType!.FullName}.{method.Name} returned null for a representative input.");
            return;
        }
        Assert.IsTrue(method.ReturnType.IsInstanceOfType(result),
            $"{method.DeclaringType!.FullName}.{method.Name} returned {result.GetType()} instead of {method.ReturnType}.");
        if (result is IEnumerable sequence && result is not string)
            Assert.IsNotNull(sequence.GetEnumerator(), $"{method.Name} must return an iterable result.");
    }
}
