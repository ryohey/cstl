using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Xml;
using System.IO;

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

public interface ICodeGeneratable
{
    string Generate();
}

public enum AccessibilityCode
{
    Public, Private, Protected, Internal
}

public static class CodeUtils
{
    public static string Generate(this AccessibilityCode self)
    {
        switch (self)
        {
            case AccessibilityCode.Internal:
                return "internal";
            case AccessibilityCode.Private:
                return "private";
            case AccessibilityCode.Protected:
                return "protected";
            case AccessibilityCode.Public:
                return "public";
        }
        return "";
    }

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

public struct PropertyCode: ICodeGeneratable
{
    public string name;
    public string type;
    public string value;
    public AccessibilityCode accessibility;

    public string Generate()
    {
        return $"{accessibility.Generate()} {type} {name} = {value};";
    }
}

public struct ArgumentCode: ICodeGeneratable
{
    public string name;
    public string type;
    public string defaultValue;

    public string Generate()
    {
        return $"{type} {name} = {defaultValue}";
    }
}

public struct MethodCode: ICodeGeneratable
{
    public string name;
    public string returnType;
    public ArgumentCode[] arguments;
    public AccessibilityCode accessibility;
    public string body;

    public string Generate()
    {
        var args = string.Join(", ", arguments.Select(arg => arg.Generate()));
        return CodeUtils.Lines(
            $"{accessibility.Generate()} {returnType} {name}({args})",
            CodeUtils.Block(body)
        );
    }
}

public struct ClassCode: ICodeGeneratable
{
    public string name;
    public string baseClassName;
    public AccessibilityCode accessibility;
    public PropertyCode[] properties;
    public MethodCode[] methods;

    public string Generate()
    {
        var propString = CodeUtils.Lines(properties.Select(prop => prop.Generate()).ToArray());
        var methodString = CodeUtils.Lines(methods.Select(method => method.Generate()).ToArray(), 2);

        return CodeUtils.Lines(
            $"{accessibility.Generate()} class {name}: {baseClassName}",
            CodeUtils.Block(CodeUtils.Lines(2, propString, methodString))
        );
    }
}

public struct MonoBehaviourCode : ICodeGeneratable
{
    public string name;
    public Tag[] components;

    public string Generate()
    {
        int generatePropCount = 0;
        string GeneratePropName()
        {
            return $"__Prop{generatePropCount++}";
        }

        string GenerateAddComponentCode(Tag tag, string propName)
        {
            var add = $"{propName} = gameObject.AddComponent<{tag.tagName}>();";
            var attrs = tag.attributes.Select(attr => $"{propName}.{attr.Key} = {attr.Value};").ToArray();

            return CodeUtils.Lines(add, CodeUtils.Lines(attrs));
        }

        var props = components.Select(c => new PropertyCode { 
            name = GeneratePropName(), 
            accessibility = AccessibilityCode.Private, 
            type = c.tagName,
            value = "null",
        }).ToArray();

        var componentCodes = components.Select((c, i) => GenerateAddComponentCode(c, props[i].name)).ToArray();

        return new ClassCode
        {
            name = name,
            baseClassName = "MonoBehaviour",
            accessibility = AccessibilityCode.Public,
            properties = props,
            methods = new MethodCode[]
            {
                new MethodCode
                {
                    name = "Awake",
                    arguments = new ArgumentCode[]{},
                    accessibility = AccessibilityCode.Public,
                    returnType = "void",
                    body = CodeUtils.Lines(componentCodes)
                }
            }
        }.Generate();
    }
}

public class Castle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var filePath = Path.Combine(Application.streamingAssetsPath, "Components", "Example.cstl");
        var text = File.ReadAllText(filePath);
        var tree = Parse(text);
        var code = GenerateCode(tree);
        Debug.Log(code.Generate());
    }

    private static Tree<Tag> Parse(string text)
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
            element = new Tag { 
                tagName = node.Name,
                attributes = attributes
            },
            children = node.ChildNodes.Cast<XmlNode>().Select(GenerateTagTree).ToArray()
        };
    }

    private static PropertyCode GeneratePropertyCode(KeyValuePair<string, string> attribute)
    {
        return new PropertyCode
        {
            name = attribute.Key,
            value = attribute.Value,
            accessibility = AccessibilityCode.Public
        };
    }

    private static int generateClassCount;
    private static string GenerateClassName()
    {
        return $"__Class{generateClassCount++}";
    }

    private static ICodeGeneratable GenerateCode(Tree<Tag> entry)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
