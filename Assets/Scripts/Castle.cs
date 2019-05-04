using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using PropertyCode = System.Collections.Generic.KeyValuePair<string, string>;

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

public struct TagCode
{

}

public interface IComponentCode
{
    string ClassName { get; }
    PropertyCode[] Properties { get; }
}

public interface IContainerCode: IComponentCode
{
    IComponentCode[] Children { get; }
}

public struct ClassCode: IComponentCode
{
    public string className;
    public string ClassName => className;

    public PropertyCode[] properties;
    public PropertyCode[] Properties => properties;
}

public struct ContainerCode: IContainerCode
{
    public string className;
    public string ClassName => className;

    public PropertyCode[] properties;
    public PropertyCode[] Properties => properties;

    public IComponentCode[] children;
    public IComponentCode[] Children => children;
}

public class Castle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var text = @"
            <GameObject>
                <Rigidbody />
              <Transform localPosition=@position>
                <GameObject>
                    <MeshRenderer materials={[
                        <Material color=@color />
                    ]} />
                </GameObject>
            </GameObject>
        ";

        var tree = Parse(text);

    }

    private static Tree<Tag> Parse(string text)
    {
        return new Tree<Tag>
        {
            element = new Tag { tagName = "GameObject" },
            children = new Tree<Tag>[] { new Tree<Tag> {
                element = new Tag { tagName = "Rigidbody", attributes = new Dictionary<string, string>() } }
            }
        };
    }

    private static IComponentCode GenerateComponentCode(Tag tag, Tree<Tag>[] children)
    {
        if (tag.tagName == "GameObject")
        {

        }

        return new ComponentCode { 
            className = tag.tagName,
            properties = tag.attributes.ToList().Select(GeneratePropertyCode).ToArray()
        };
    }

    private static PropertyCode GeneratePropertyCode(KeyValuePair<string, string> attribute)
    {
        return attribute;
    }

    private static IComponentCode GenerateCode(Tree<Tag> tag)
    {
        var component = GenerateComponentCode(tag.element, tag.children);

        if (component is ContainerCode container)
        {
            container.children = tag.children.Select(GenerateCode).ToArray();
        }

        return component;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
