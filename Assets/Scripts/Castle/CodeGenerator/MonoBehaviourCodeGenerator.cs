using System.Collections.Generic;
using System.Linq;

namespace Castle
{
    public class MonoBehaviourCodeGenerator
    {
        private readonly string className;
        private readonly string templatePath;

        public MonoBehaviourCodeGenerator(string className, string templatePath)
        {
            this.className = className;
            this.templatePath = templatePath;
        }

        public bool TestTagName(string tagName)
        {
            return tagName == "GameObject";
        }

        public MonoBehaviourCode Generate(Tree<Tag> entry, CodeGeneratorContext context)
        {
            context.AddUsing("UnityEngine");
            context.AddUsing("UnityEngine.UI");

            var components = new List<MonoBehaviourCode.ChildComponent>();
            var gameObjectCodes = new List<MonoBehaviourCode>();
            var i = 0;

            foreach (var c in entry.children)
            {
                if (c.element.tagName == "GameObject")
                {
                    var name = $"MonoBehaviour_{className}_{i++}";

                    components.Add(new MonoBehaviourCode.ChildComponent
                    {
                        className = name,
                        properties = c.element.attributes,
                        isGameObject = true
                    });

                    gameObjectCodes.Add(new MonoBehaviourCodeGenerator(name, templatePath).Generate(c, context));
                }
                else
                {
                    components.Add(new MonoBehaviourCode.ChildComponent
                    {
                        className = c.element.tagName,
                        properties = c.element.attributes,
                        isGameObject = false
                    });
                }
            }

            return new MonoBehaviourCode
            {
                name = className,
                templatePath = templatePath,
                components = components.ToArray(),
                children = gameObjectCodes.ToArray()
            };
        }
    }
}
