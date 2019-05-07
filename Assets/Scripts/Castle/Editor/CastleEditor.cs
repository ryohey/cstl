using UnityEngine;
using UnityEditor;
using CastleGenerated;

namespace CastleEditor
{
    [CustomEditor(typeof(CastleMonoBehaviour))]
    public class CastleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Apply"))
            {
                Debug.Log("Pressed");
            }

            if (GUILayout.Button("Reset"))
            {
                Debug.Log("Pressed");
            }

            GUILayout.EndHorizontal();
        }
    }
}
