using System.Linq;

namespace Castle
{
    public struct MonoBehaviourCode
    {
        public string name;
        public string templatePath;
        public Tag[] components;

        public ClassCode Generate()
        {
            int generatePropCount = 0;
            string GeneratePropName()
            {
                return $"__Prop{generatePropCount++}";
            }

            string GenerateAddComponentCode(Tag tag, string propName)
            {
                var get = $"{propName} = gameObject.GetComponent<{tag.tagName}>();";
                var add = $"{propName} = {propName} != null ? {propName} : gameObject.AddComponent<{tag.tagName}>();";
                var attrs = tag.attributes.Select(attr => $"{propName}.{attr.Key} = {attr.Value};").ToArray();

                return CodeUtils.Lines(get, add, CodeUtils.Lines(attrs));
            }

            var props = components.Select(c => new PropertyCode
            {
                name = GeneratePropName(),
                accessibility = AccessibilityCode.Private,
                type = c.tagName
            }).ToList();

            props.Add(new PropertyCode
            {
                name = "TemplatePath",
                type = "string",
                accessibility = AccessibilityCode.Public,
                isOverride = true,
                getter = $"return \"{templatePath}\";"
            });

            var componentCodes = components.Select((c, i) => GenerateAddComponentCode(c, props[i].name)).ToArray();

            return new ClassCode
            {
                name = name,
                attributes = new string[] { "ExecuteAlways" },
                baseClassName = "CastleMonoBehaviour",
                accessibility = AccessibilityCode.Public,
                properties = props.ToArray(),
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
