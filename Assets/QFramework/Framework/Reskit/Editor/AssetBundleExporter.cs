using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace QFramework
{
    public class AssetBundleExporter : Editor
    {
        [UnityEditor.MenuItem("QFramework/Framework/ResKit/Build AssetBundles", false)]
        static void BuildAssetBundles()
        {
            var outputPath = Application.streamingAssetsPath + "/AssetBundles/" + ReskitUtil.GetPlatformName();

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression,
                EditorUserBuildSettings.activeBuildTarget);
            
            AssetDatabase.Refresh();
        }
    }
}