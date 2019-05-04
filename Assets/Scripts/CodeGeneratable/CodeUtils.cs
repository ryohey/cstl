using System;
using System.Linq;

namespace Castle
{
    public static class CodeUtils
    {
        public static string Lines(string[] lines, int numberOfNewlines = 1)
        {
            var newline = string.Concat(Enumerable.Repeat(Environment.NewLine, numberOfNewlines));
            return string.Join(newline, lines);
        }

        public static string Lines(int numberOfNewlines, params string[] lines)
        {
            return Lines(lines, numberOfNewlines);
        }

        public static string Lines(params string[] lines)
        {
            return Lines(1, lines);
        }

        public static string Indent(string text, string indentString = "    ", int level = 1)
        {
            var indent = string.Concat(Enumerable.Repeat(indentString, level));
            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            return Lines(lines.Select(line => indent + line).ToArray());
        }

        public static string Block(string text)
        {
            return Lines("{", Indent(text), "}");
        }
    }
}
