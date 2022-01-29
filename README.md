# cstl

Unity GameObject Code Generation with React-like XML syntax.

This is an experimental project. It is not suitable for any project.

## cstl Code

```xml
<GameObject>
    <Rigidbody 
        mass="@(1)"
        drag="@(3)"
        isKinematic="@(true)"
    />
    <Transform 
        localPosition="@(new Vector3(1, 212, 31))" 
        localScale="@(new Vector3(1, 21, 22))"
    />
    <MeshRenderer />
</GameObject>
```

### Generated Script

```cs
using UnityEngine;
using UnityEngine.UI;

namespace CastleGenerated
{
    [ExecuteAlways]
    public class Example: CastleMonoBehaviour
    {
        
        
        private  Rigidbody __Prop0;
        private  Transform __Prop1;
        private  MeshRenderer __Prop2;
        public override string TemplatePath
        {
            get
            {
                return "Assets/StreamingAssets/Components/Example.cstl";
            }
        }
        
        public override void SetupComponents ()
        {
            __Prop0 = gameObject.GetComponent<Rigidbody>();
            __Prop0 = __Prop0 != null ? __Prop0 : gameObject.AddComponent<Rigidbody>();
            __Prop0.mass = 1;
            __Prop0.drag = 3;
            __Prop0.isKinematic = true;
            __Prop1 = gameObject.GetComponent<Transform>();
            __Prop1 = __Prop1 != null ? __Prop1 : gameObject.AddComponent<Transform>();
            __Prop1.localPosition = new Vector3(1, 212, 31);
            __Prop1.localScale = new Vector3(1, 21, 22);
            __Prop2 = gameObject.GetComponent<MeshRenderer>();
            __Prop2 = __Prop2 != null ? __Prop2 : gameObject.AddComponent<MeshRenderer>();
            
        }
        
        public  void Awake ()
        {
            SetupComponents();
        }
    }
}
```

## Syntax

```xml
<ClassName 
    propName="string value"
    anotherProp="@(1 + 2)"
>
    <ChildClassName />
</ClassName>
```

`"@()"` is treated as a C# script.

GameObject is the only Class that can have children.

