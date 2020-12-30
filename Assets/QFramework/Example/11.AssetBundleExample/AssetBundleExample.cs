using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace QFramework
{
    public class AssetBundleExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/11.AssetBundleExample/Build AssetBundle", false, 11)]
        private static void MenuItem()
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }

            UnityEditor.BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath,
                UnityEditor.BuildAssetBundleOptions.None, UnityEditor.BuildTarget.StandaloneWindows);
        }

        [UnityEditor.MenuItem("QFramework/Example/11.AssetBundleExample/Run", false, 11)]
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
            
            HotUpdateMgr.Instance.CheckState(() =>
            {
                mBundle = mResLoader.LoadSync<AssetBundle>("gameobject");
                var obj = mBundle.LoadAsset<GameObject>("GameObject");
                
                Instantiate(obj);
            });
            
            // Application.OpenURL(Application.persistentDataPath);
            //
            // bool finished = false;
            //
            // HotUpdateMgr.Instance.CheckState(() =>
            // {
            //     Debug.Log(HotUpdateMgr.Instance.State);
            //
            //     HotUpdateMgr.Instance.HasNewVersionRes(needUpdate =>
            //     {
            //         if (needUpdate)
            //         {
            //             HotUpdateMgr.Instance.UpdateRes(() =>
            //             {
            //                 Debug.Log("热更结束");
            //                 Debug.Log("继续");
            //                 finished = true;
            //             });
            //         }
            //         else
            //         {
            //             Debug.Log("不需要热更");
            //             Debug.Log("继续");
            //             finished = true;
            //         }
            //     });
            // });
            
            // while (!finished)
            // {
            //     yield return null;
            // }

        }

        private void OnDestroy()
        {
            mBundle = null;
            mResLoader.ReleaseAll();
            mResLoader = null;
        }
    }
}