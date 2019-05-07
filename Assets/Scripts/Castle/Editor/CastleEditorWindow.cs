using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CastleEditor
{
    public class CastleEditorWindow : EditorWindow
    {
        [SerializeField]
        private GeneratorController controller = new GeneratorController();

        [MenuItem("Window/Castle")]
        private static void Create()
        { 
            GetWindow<CastleEditorWindow>("Castle");
        }

        private void OnGUI()
        {
            controller.projectDir = Application.dataPath;

            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            GUILayout.Label("Input Directory");
            if (GUILayout.Button("Open"))
            {
                controller.inputDir = EditorUtility.OpenFolderPanel("Open Input Directory", "", "");
            }
            GUILayout.EndHorizontal();
            GUILayout.Label(controller.inputDir);

            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            GUILayout.Label("Output Directory");
            if (GUILayout.Button("Open"))
            {
                controller.outputDir = EditorUtility.OpenFolderPanel("Open Output Directory", "", "");
            }
            GUILayout.EndHorizontal();
            GUILayout.Label(controller.outputDir);

            if (GUILayout.Button("Generate"))
            {
                controller.Generate();
            }

            if (GUILayout.Button("Watch"))
            {
                controller.Watch();
            }

            if (GUILayout.Button("Stop Watching"))
            {
                controller.StopWatching();
            }
        }
    }
}
