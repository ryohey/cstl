using UnityEngine;

namespace CastleGenerated
{
    public class __Class0: MonoBehaviour
    {
        private Rigidbody __Prop0 = null;
        private Transform __Prop1 = null;
        
        public void Awake()
        {
            __Prop0 = gameObject.GetComponent<Rigidbody>();
            __Prop0 = __Prop0 ?? gameObject.AddComponent<Rigidbody>();
            __Prop0.mass = 1;
            __Prop0.isKinematic = true;
            __Prop1 = gameObject.GetComponent<Transform>();
            __Prop1 = __Prop1 ?? gameObject.AddComponent<Transform>();
            __Prop1.localPosition = new Vector3(1, 2, 3);
            __Prop1.localScale = new Vector3(1, 1, 1);
        }
    }
}