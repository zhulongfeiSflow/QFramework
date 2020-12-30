using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace QFramework
{
    public class AssetBundleExporter : Editor
    {
        [UnityEditor.MenuItem("QFramework/Framework/ResKit/Build AssetBundles", false)]
        static void BuildAssetBundles()
        {
            var outputPath = Application.streamingAssetsPath + "/AssetBundles/" + ResKitUtil.GetPlatformName();

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression,
                EditorUserBuildSettings.activeBuildTarget);

            var versionConfigFilepath = outputPath + "/ResVersion.json";

            var resVersion = new ResVersion()
            {
                Version = 15,
                AssetBundleNames = AssetDatabase.GetAllAssetBundleNames().ToList(),
            };

            var resVersionJson = JsonUtility.ToJson(resVersion, true);
            
            File.WriteAllText(versionConfigFilepath, resVersionJson);
            
            AssetDatabase.Refresh();
        }
    }
}