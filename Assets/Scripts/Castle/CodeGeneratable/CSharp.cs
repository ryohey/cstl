using System.Collections.Generic;
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

    public struct PropertyCode
    {
        public string name;
        public string type;
        public string value;
        public string getter;
        public string setter;
        public bool isOverride;
        public AccessibilityCode accessibility;

        public string Generate()
        {
            var overrideString = isOverride ? "override" : null;
            var arr = new List<string> { accessibility.Generate(), overrideString, type, name };

            if (!string.IsNullOrEmpty(value))
            {
                arr.Add("=");
                arr.Add(value);
            }

            var body = string.Join(" ", arr.ToArray());

            if (!string.IsNullOrEmpty(getter) || !string.IsNullOrEmpty(setter))
            {
                var blocks = new List<string>();
                if (!string.IsNullOrEmpty(getter))
                {
                    blocks.Add(CodeUtils.Lines("get", CodeUtils.Block(getter)));
                }
                if (!string.IsNullOrEmpty(setter))
                {
                    blocks.Add(CodeUtils.Lines("set", CodeUtils.Block(setter)));
                }
                body = CodeUtils.Lines(body, CodeUtils.Block(CodeUtils.Lines(blocks.ToArray())));
            }
            else
            {
                body += ";";
            }

            return body;
        }
    }

    public struct ArgumentCode
    {
        public string name;
        public string type;
        public string defaultValue;

        public string Generate()
        {
            return $"{type} {name} = {defaultValue}";
        }
    }

    public struct MethodCode
    {
        public string name;
        public string returnType;
        public bool isOverride;
        public ArgumentCode[] arguments;
        public AccessibilityCode accessibility;
        public string body;

        public string Generate()
        {
            var overrideString = isOverride ? "override" : null;
            var args = string.Join(", ", arguments.Select(arg => arg.Generate()));
            return CodeUtils.Lines(
                string.Join(" ", new string[] {
                    accessibility.Generate(),
                    overrideString,
                    returnType,
                    name,
                    $"({args})"
                }),
                CodeUtils.Block(body)
            );
        }
    }

    public struct ClassCode
    {
        public string[] attributes;
        public string name;
        public string baseClassName;
        public AccessibilityCode accessibility;
        public PropertyCode[] properties;
        public MethodCode[] methods;

        public string Generate()
        {
            var attributeString = CodeUtils.Lines(attributes.Select(attr => $"[{attr}]").ToArray());
            var propString = CodeUtils.Lines(properties.Select(prop => prop.Generate()).ToArray());
            var methodString = CodeUtils.Lines(methods.Select(method => method.Generate()).ToArray(), 2);

            return CodeUtils.Lines(
                attributeString,
                $"{accessibility.Generate()} class {name}: {baseClassName}",
                CodeUtils.Block(CodeUtils.Lines(2, propString, methodString))
            );
        }
    }

    public struct SourceFileCode
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
