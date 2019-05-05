using System.Linq;

namespace Castle
{
    public class MonoBehaviourCodeGenerator : IPartialCodeGenerator
    {
        private readonly string className;

        public MonoBehaviourCodeGenerator(string className)
        {
            this.className = className;
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
                components = entry.children.Select(c => c.element).ToArray()
            }.Generate();
        }
    }
}
