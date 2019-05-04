using UnityEngine;
using System.IO;

namespace CastleGame
{
    public class TestGeneration : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var filePath = Path.Combine(Application.streamingAssetsPath, "Components", "Example.cstl");
            var text = File.ReadAllText(filePath);
            var tree = Castle.Parser.Parse(text);
            var code = new Castle.CodeGenerator().GenerateCode(tree);
            Debug.Log(code.Generate());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
