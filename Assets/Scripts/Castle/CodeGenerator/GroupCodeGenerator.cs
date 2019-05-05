using System.Linq;

namespace Castle
{
    public class GroupCodeGenerator : IPartialCodeGenerator
    {
        readonly private IPartialCodeGenerator[] generators;

        public GroupCodeGenerator(IPartialCodeGenerator[] generators)
        {
            this.generators = generators;
        }

        public ClassCode Generate(Tree<Tag> entry, CodeGeneratorContext context)
        {
            return generators
                .First(gen => gen.TestTagName(entry.element.tagName))
                .Generate(entry, context);
        }

        public bool TestTagName(string tagName)
        {
            return generators.FirstOrDefault(gen => gen.TestTagName(tagName)) != null;
        }
    }
}
