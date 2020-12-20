using System;
using System.IO;
using UnityEngine;

namespace QFramework
{
    public class AssetBundleExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/24.AssetBundleExample/Build AssetBundle", false, 24)]
        private static void MenuItem()
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }

            UnityEditor.BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath,
                UnityEditor.BuildAssetBundleOptions.None, UnityEditor.BuildTarget.StandaloneWindows);
        }

        [UnityEditor.MenuItem("QFramework/Example/24.AssetBundleExample/Run", false, 24)]
        private static void MenuItem2()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("AssetBundleExample").AddComponent<AssetBundleExample>();
        }
#endif

        private ResLoader mResLoader = new ResLoader();

        private AssetBundle mBundle;

        private void Start()
        {
            mBundle = mResLoader.LoadSync<AssetBundle>("gameobject");
            var obj = mBundle.LoadAsset<GameObject>("GameObject");

            Instantiate(obj);
        }

        private void OnDestroy()
        {
            mBundle = null;
            mResLoader.ReleaseAll();
            mResLoader = null;
        }
    }
}