using UnityEngine;
using UnityEditor;

namespace CastleEditor
{
    public static class WakeOnCompile
    {
        [InitializeOnLoadMethod]
        static void OnLoad()
        {
            EditorApplication.delayCall += RefreshComponents;
        }

        private static void RefreshComponents()
        {
            var components = GameObject.FindObjectsOfType<CastleGenerated.CastleMonoBehaviour>();
            foreach (var component in components)
            {
                component.SetupComponents();
            }
            Debug.Log($"Reload {components.Length} components.");
        }
    }
}
