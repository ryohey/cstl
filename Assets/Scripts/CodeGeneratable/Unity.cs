using System.Linq;

namespace Castle
{
    public struct MonoBehaviourCode : ICodeGeneratable
    {
        public string name;
        public Tag[] components;

        public string Generate()
        {
            int generatePropCount = 0;
            string GeneratePropName()
            {
                return $"__Prop{generatePropCount++}";
            }

            string GenerateAddComponentCode(Tag tag, string propName)
            {
                var add = $"{propName} = gameObject.AddComponent<{tag.tagName}>();";
                var attrs = tag.attributes.Select(attr => $"{propName}.{attr.Key} = {attr.Value};").ToArray();

                return CodeUtils.Lines(add, CodeUtils.Lines(attrs));
            }

            var props = components.Select(c => new PropertyCode
            {
                name = GeneratePropName(),
                accessibility = AccessibilityCode.Private,
                type = c.tagName,
                value = "null",
            }).ToArray();

            var componentCodes = components.Select((c, i) => GenerateAddComponentCode(c, props[i].name)).ToArray();

            return new ClassCode
            {
                name = name,
                baseClassName = "MonoBehaviour",
                accessibility = AccessibilityCode.Public,
                properties = props,
                methods = new MethodCode[]
                {
                new MethodCode
                {
                    name = "Awake",
                    arguments = new ArgumentCode[]{},
                    accessibility = AccessibilityCode.Public,
                    returnType = "void",
                    body = CodeUtils.Lines(componentCodes)
                }
                }
            }.Generate();
        }
    }
}
