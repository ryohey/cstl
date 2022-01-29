using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Castle
{ 
    public struct MonoBehaviourCode
    {
        public struct ChildComponent
        {
            public string className;
            public Dictionary<string, string> properties;
            public bool isGameObject;
        }

        public string name;
        public string templatePath;
        public ChildComponent[] components;
        public MonoBehaviourCode[] children;

        public ClassCode Generate()
        {
            int generatePropCount = 0;
            string GeneratePropName()
            {
                return $"__Prop{generatePropCount++}";
            }

            string GeneratePropCode(string name, IDictionary<string, string> properties)
            {
                string ParseValue(string value)
                {
                    var propReg = new Regex(@"@\((.+)\)");
                    var match = propReg.Match(value);
                    if (match.Success)
                    {
                        // evaluate as C# script
                        return match.Groups[1].Value;
                    }
                    // evaluate as string
                    return $"\"{value}\"";
                }

                var attrs = properties.Select(attr => $"{name}.{attr.Key} = {ParseValue(attr.Value)};").ToArray();
                return CodeUtils.Lines(attrs);
            }

            string GenerateAddComponentCode(ChildComponent component, string propName)
            {
                var get = $"{propName} = gameObject.GetComponent<{component.className}>();";
                var add = $"{propName} = {propName} != null ? {propName} : gameObject.AddComponent<{component.className}>();";
                var attrs = GeneratePropCode(propName, component.properties);

                return CodeUtils.Lines(get, add, attrs);
            }

            string GenerateInstantiateCode(ChildComponent component, string propName)
            {
                var get = $"{propName} = gameObject.GetComponentInChildren<{component.className}>();";
                var add = $"{propName} = {propName} != null ? {propName} : Instantiate(new GameObject(), transform).AddComponent<{component.className}>();";
                var attrs = GeneratePropCode(propName, component.properties);

                return CodeUtils.Lines(get, add, CodeUtils.Lines(attrs));
            }

            string GenerateSetupPropertyCode(ChildComponent component, string propName)
            {
                if (component.isGameObject)
                {
                    return GenerateInstantiateCode(component, propName);
                }
                return GenerateAddComponentCode(component, propName);
            }

            var props = components.Select(c => new PropertyCode
            {
                name = GeneratePropName(),
                accessibility = AccessibilityCode.Private,
                type = c.className
            }).ToList();

            props.Add(new PropertyCode
            {
                name = "TemplatePath",
                type = "string",
                accessibility = AccessibilityCode.Public,
                isOverride = true,
                getter = $"return \"{templatePath}\";"
            });

            var componentCodes = components
                .Select((c, i) => GenerateSetupPropertyCode(c, props[i].name))
                .ToArray();

            return new ClassCode
            {
                name = name,
                attributes = new string[] { "ExecuteAlways" },
                baseClassName = "CastleMonoBehaviour",
                accessibility = AccessibilityCode.Public,
                properties = props.ToArray(),
                classes = children.Select(c => c.Generate()).ToArray(),
                methods = new MethodCode[]
                {
                    new MethodCode
                    {
                        name = "SetupComponents",
                        isOverride = true,
                        arguments = new ArgumentCode[]{},
                        accessibility = AccessibilityCode.Public,
                        returnType = "void",
                        body = CodeUtils.Lines(componentCodes)
                    },
                    new MethodCode
                    {
                        name = "Awake",
                        arguments = new ArgumentCode[]{ },
                        accessibility = AccessibilityCode.Public,
                        returnType = "void",
                        body = "SetupComponents();"
                    }
                }
            };
        }
    }
}
