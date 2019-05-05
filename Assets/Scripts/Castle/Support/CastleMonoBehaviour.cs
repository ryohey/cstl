using UnityEngine;

namespace CastleGenerated
{
    public class CastleMonoBehaviour : MonoBehaviour
    {
        public virtual void SetupComponents()
        {
        }

        protected void RemoveAllComponents()
        {
            foreach (var component in gameObject.GetComponents<Component>())
            {
                if (!(component is Transform) && !(component is CastleMonoBehaviour))
                {
                    DestroyImmediate(component);
                }
            }
        }
    }
}
