using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

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
            var dirFiles = Directory.GetFiles(inputDir);
            var componentFiles = new List<string>(dirFiles).Where(f => Path.GetExtension(f) == ".cstl");

            foreach (var path in componentFiles)
            {
                var text = File.ReadAllText(path);
                var tree = Castle.Parser.Parse(text);
                var result = Castle.CodeGenerator.Generate(tree);
                var outputPath = Path.Combine(outputDir, Path.ChangeExtension(Path.GetFileName(path), "cs"));
                File.WriteAllText(outputPath, code);
            }
        }

        if (GUILayout.Button("Watch"))
        {
            Debug.Log("Pressed");
        }
    }
}