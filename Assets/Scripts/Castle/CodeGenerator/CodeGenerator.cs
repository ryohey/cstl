﻿using System.Collections.Generic;
using System.Linq;

namespace Castle
{
    public interface IPartialCodeGenerator
    {
        bool TestTagName(string tagName);
        ClassCode Generate(Tree<Tag> entry, CodeGeneratorContext context);
    }

    public static class CodeGenerator
    {
        public static ICodeGeneratable Generate(Tree<Tag> tree)
        {
            var codeGen = new GroupCodeGenerator(new IPartialCodeGenerator[] { 
                new MonoBehaviourCodeGenerator() 
            });
            var context = new CodeGeneratorContext();
            var classCode = codeGen.Generate(tree, context);

            var sourceCode = new SourceFileCode
            {
                usingNames = context.usingNames.ToArray(),
                namespaceString = "CastleGenerated",
                classes = new ClassCode[] { classCode }
            };

            return sourceCode;
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