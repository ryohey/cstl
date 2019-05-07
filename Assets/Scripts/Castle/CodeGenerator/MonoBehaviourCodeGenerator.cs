using System.Linq;

namespace Castle
{
    public class MonoBehaviourCodeGenerator : IPartialCodeGenerator
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

        public ClassCode Generate(Tree<Tag> entry, CodeGeneratorContext context)
        {
            context.AddUsing("UnityEngine");

            return new MonoBehaviourCode
            {
                name = className,
                templatePath = templatePath,
                components = entry.children.Select(c => c.element).ToArray()
            }.Generate();
        }
    }
}
