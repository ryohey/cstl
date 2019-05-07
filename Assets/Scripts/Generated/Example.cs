using UnityEngine;

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