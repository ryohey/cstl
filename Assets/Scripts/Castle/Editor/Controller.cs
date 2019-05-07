using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace CastleEditor
{
    [Serializable]
    public class GeneratorController: ISerializationCallbackReceiver
    {
        [SerializeField]
        public string inputDir = "";

        [SerializeField]
        public string outputDir = "";

        [SerializeField]
        public string projectDir = "";

        [SerializeField]
        public bool isWatching = false;

        private FileSystemWatcher watcher;

        private bool CheckCondition()
        {
            if (inputDir.Length == 0)
            {
                Debug.LogWarning("Input directory is not set");
                return false;
            }
            if (outputDir.Length == 0)
            {
                Debug.LogWarning("Output directory is not set");
                return false;
            }
            if (!Directory.Exists(inputDir))
            {
                Debug.LogWarning("Input directory does not exist");
                return false;
            }
            if (!Directory.Exists(outputDir))
            {
                Debug.LogWarning("Output directory does not exist");
                return false;
            }
            if (!Directory.Exists(projectDir))
            {
                Debug.LogWarning("Projcet directory does not exist");
                return false;
            }

            return true;
        }

        public void Generate()
        {
            if (!CheckCondition())
            {
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
                GenerateCode(path);
            }
        }

        private void GenerateCode(string path)
        {
            var text = File.ReadAllText(path);
            var tree = Castle.Parser.Parse(text);
            var relPath = PathUtils.GetRelativePath(path, projectDir);
            var code = Castle.CodeGenerator.Generate(tree, Path.GetFileNameWithoutExtension(path), relPath);
            var outputPath = GetOutputPath(path);
            File.WriteAllText(outputPath, code);
            Debug.Log($"Generated to {outputPath}");
        }

        private string GetOutputPath(string path)
        {
            return Path.Combine(outputDir, Path.ChangeExtension(Path.GetFileName(path), "cs"));
        }

        public void Watch()
        {
            if (!CheckCondition())
            {
                return;
            }

            StopWatching();
            watcher = new FileSystemWatcher(inputDir);

            watcher.Deleted += (sender, e) =>
            {
                Debug.Log($"Deletion detected: {e.Name}");
                var path = Path.Combine(inputDir, e.Name);
                var outputPath = GetOutputPath(path);
                File.Delete(outputPath);
            };

            watcher.Created += (sender, e) =>
            {
                Debug.Log($"Creation detected: {e.Name}");
                var path = Path.Combine(inputDir, e.Name);
                GenerateCode(path);
            };

            watcher.Changed += (sender, e) =>
            {
                Debug.Log($"Changes detected: {e.Name}");
                var path = Path.Combine(inputDir, e.Name);
                GenerateCode(path);
            };

            watcher.EnableRaisingEvents = true;
            isWatching = true;

            Debug.Log("Start watching");
        }

        public void StopWatching()
        {
            if (watcher == null)
            {
                return;
            }
            watcher.Dispose();
            watcher = null;
            isWatching = false;
            Debug.Log("Stop watching");
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Debug.Log("Controller state is restored");
            if (isWatching)
            {
                Watch();
            }
        }
    }
}
