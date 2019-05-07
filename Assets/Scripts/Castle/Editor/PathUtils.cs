using System;

namespace CastleEditor
{
    public static class PathUtils
    {
        public static string GetRelativePath(string filePath, string basePath)
        {
            Uri baseUri = new Uri(basePath.Replace("%", "%25"));
            Uri fileUri = new Uri(filePath.Replace("%", "%25"));
            Uri relativeUri = baseUri.MakeRelativeUri(fileUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());
            return relativePath.Replace("%25", "%");
        }
    }
}
