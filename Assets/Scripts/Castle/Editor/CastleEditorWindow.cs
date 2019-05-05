using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CastleEditor
{
    public class CastleEditorWindow : EditorWindow
    {
        private string inputDir = "";
        private string outputDir = "";

        [MenuItem("Window/Castle")]
        private static void Create()
        {
            GetWindow<CastleEditorWindow>("Castle");
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            GUILayout.Label("Input Directory");
            if (GUILayout.Button("Open"))
            {
                inputDir = EditorUtility.OpenFolderPanel("Open Input Directory", "", "");
            }
            GUILayout.EndHorizontal();
            GUILayout.Label(inputDir);

            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            GUILayout.Label("Output Directory");
            if (GUILayout.Button("Open"))
            {
                outputDir = EditorUtility.OpenFolderPanel("Open Output Directory", "", "");
            }
            GUILayout.EndHorizontal();
            GUILayout.Label(outputDir);

            if (GUILayout.Button("Generate"))
            {
                if (inputDir.Length == 0)
                {
                    Debug.LogWarning("Input directory is not set");
                    return;
                }
                if (outputDir.Length == 0)
                {
                    Debug.LogWarning("Output directory is not set");
                    return;
                }
                if (!Directory.Exists(inputDir))
                {
                    Debug.LogWarning("Input directory does not exist");
                    return;
                }
                if (!Directory.Exists(outputDir))
                {
                    Debug.LogWarning("Output directory does not exist");
                    return;
                }

                var dirFiles = Directory.GetFiles(inputDir);
                var componentFiles = new List<string>(dirFiles).Where(f => Path.GetExtension(f) == ".cstl");

                if (componentFiles.Count() == 0)
                {
                    Debug.LogWarning("There are no .cstl files in input directory");
                    return;
                }

                foreach (var path in componentFiles)
                {
                    var text = File.ReadAllText(path);
                    var tree = Castle.Parser.Parse(text);
                    var code = Castle.CodeGenerator.Generate(tree, Path.GetFileNameWithoutExtension(path));
                    var outputPath = Path.Combine(outputDir, Path.ChangeExtension(Path.GetFileName(path), "cs"));
                    File.WriteAllText(outputPath, code);
                    Debug.Log($"Generated to {outputPath}");
                }
            }

            if (GUILayout.Button("Watch"))
            {
                Debug.Log("Pressed");
            }
        }
    }
}
