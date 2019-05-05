using System.Linq;

namespace Castle
{
    public enum AccessibilityCode
    {
        Public, Private, Protected, Internal
    }

    public static class AccessibilityCodeExtensions
    {
        public static string Generate(this AccessibilityCode self)
        {
            switch (self)
            {
                case AccessibilityCode.Internal:
                    return "internal";
                case AccessibilityCode.Private:
                    return "private";
                case AccessibilityCode.Protected:
                    return "protected";
                case AccessibilityCode.Public:
                    return "public";
            }
            return "";
        }
    }

    public struct PropertyCode : ICodeGeneratable
    {
        public string name;
        public string type;
        public string value;
        public AccessibilityCode accessibility;

        public string Generate()
        {
            return $"{accessibility.Generate()} {type} {name} = {value};";
        }
    }

    public struct ArgumentCode : ICodeGeneratable
    {
        public string name;
        public string type;
        public string defaultValue;

        public string Generate()
        {
            return $"{type} {name} = {defaultValue}";
        }
    }

    public struct MethodCode : ICodeGeneratable
    {
        public string name;
        public string returnType;
        public ArgumentCode[] arguments;
        public AccessibilityCode accessibility;
        public string body;

        public string Generate()
        {
            var args = string.Join(", ", arguments.Select(arg => arg.Generate()));
            return CodeUtils.Lines(
                $"{accessibility.Generate()} {returnType} {name}({args})",
                CodeUtils.Block(body)
            );
        }
    }

    public struct ClassCode : ICodeGeneratable
    {
        public string name;
        public string baseClassName;
        public AccessibilityCode accessibility;
        public PropertyCode[] properties;
        public MethodCode[] methods;

        public string Generate()
        {
            var propString = CodeUtils.Lines(properties.Select(prop => prop.Generate()).ToArray());
            var methodString = CodeUtils.Lines(methods.Select(method => method.Generate()).ToArray(), 2);

            return CodeUtils.Lines(
                $"{accessibility.Generate()} class {name}: {baseClassName}",
                CodeUtils.Block(CodeUtils.Lines(2, propString, methodString))
            );
        }
    }

    public struct SourceFileCode : ICodeGeneratable
    {
        public string[] usingNames;
        public ClassCode[] classes;
        public string namespaceString;

        public string Generate()
        {
            var usingString = CodeUtils.Lines(usingNames
                .Distinct()
                .OrderBy(name => name)
                .Select(name => $"using {name};"
            ).ToArray());

            var classString = CodeUtils.Lines(2, classes.Select(c => c.Generate()).ToArray());

            return CodeUtils.Lines(
                usingString,
                "",
                $"namespace {namespaceString}",
                CodeUtils.Block(classString)
            );
        }
    }
}
