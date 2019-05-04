using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Castle
{
    public struct Tree<T>
    {
        public T element;
        public Tree<T>[] children;
    }

    public struct Tag
    {
        public string tagName;
        public Dictionary<string, string> attributes;
    }

    public static class Parser
    {
        public static Tree<Tag> Parse(string text)
        {
            var doc = new XmlDocument();
            doc.LoadXml(text);
            return GenerateTagTree(doc.DocumentElement);
        }

        private static Tree<Tag> GenerateTagTree(XmlNode node)
        {
            var attributes = node.Attributes.Cast<XmlAttribute>()
                .ToDictionary(attr => attr.Name, attr => attr.Value);

            return new Tree<Tag>
            {
                element = new Tag
                {
                    tagName = node.Name,
                    attributes = attributes
                },
                children = node.ChildNodes.Cast<XmlNode>().Select(GenerateTagTree).ToArray()
            };
        }
    }
}
