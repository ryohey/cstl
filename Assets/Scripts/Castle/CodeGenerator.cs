using System.Linq;

namespace Castle
{
    public class CodeGenerator
    {
        private int generateClassCount;
        private string GenerateClassName()
        {
            return $"__Class{generateClassCount++}";
        }

        public ICodeGeneratable GenerateCode(Tree<Tag> entry)
        {
            var tag = entry.element;

            if (tag.tagName == "GameObject")
            {
                return new MonoBehaviourCode
                {
                    name = GenerateClassName(),
                    components = entry.children.Select(c => c.element).ToArray()
                };
            }
            return null;
        }
    }
}
