using UnityEngine;

namespace CastleGenerated
{
    
    public class Example: CastleMonoBehaviour
    {
        private Rigidbody __Prop0 = null;
        private Transform __Prop1 = null;
        
        public override void SetupComponents ()
        {
            RemoveAllComponents();
            
            __Prop0 = gameObject.GetComponent<Rigidbody>();
            __Prop0 = __Prop0 != null ? __Prop0 : gameObject.AddComponent<Rigidbody>();
            __Prop0.mass = 1;
            __Prop0.isKinematic = false;
            __Prop1 = gameObject.GetComponent<Transform>();
            __Prop1 = __Prop1 != null ? __Prop1 : gameObject.AddComponent<Transform>();
            __Prop1.localPosition = new Vector3(5, 1, 1);
            __Prop1.localScale = new Vector3(1, 1, 1);
        }
        
        public void Awake ()
        {
            SetupComponents();
        }
    }
}