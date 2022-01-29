using System.Collections.Generic;
using System.Linq;

namespace Castle
{
    public static class CodeGenerator
    {
        public static string Generate(Tree<Tag> tree, string className, string filePath)
        {
            var codeGen = new MonoBehaviourCodeGenerator(className, filePath);
            var context = new CodeGeneratorContext();
            var classCode = codeGen.Generate(tree, context);

            var sourceCode = new SourceFileCode
            {
                usingNames = context.usingNames.ToArray(),
                namespaceString = "CastleGenerated",
                classes = new ClassCode[] { classCode.Generate() }
            };

            return sourceCode.Generate();
        }
    }

    public class CodeGeneratorContext
    {
        private int generateClassCount;
        public readonly IList<string> usingNames = new List<string>();

        public void AddUsing(string name)
        {
            usingNames.Add(name);
        }

        public string GetUniqueClassName()
        {
            return $"__Class{generateClassCount++}";
        }
    }
}
