using System.Linq;

namespace Castle
{
    public class MonoBehaviourCodeGenerator : IPartialCodeGenerator
    {
        public bool TestTagName(string tagName)
        {
            return tagName == "GameObject";
        }

        public ClassCode Generate(Tree<Tag> entry, CodeGeneratorContext context)
        {
            context.AddUsing("UnityEngine");

            return new MonoBehaviourCode
            {
                name = context.GetUniqueClassName(),
                components = entry.children.Select(c => c.element).ToArray()
            }.Generate();
        }
    }
}
