using System;
using System.IO;
using UnityEngine;

namespace QFramework
{
    public class HotUpdateMgr : MonoSingleton<HotUpdateMgr>
    { 
        public int GetLocalResVersion()
        {
            var localResVersionPath = Application.streamingAssetsPath + "/AssetBundles/Windows/ResVersion.json";
            var jsonString = File.ReadAllText(localResVersionPath);
            var localResVersion = JsonUtility.FromJson<ResVersion>(jsonString);
            return localResVersion.Version;
        }

        public void HasNewVersionRes(Action<bool> onResultGetted)
        {
            FakeResServer.Instance.GetRemoteResVersion(remoteResVersion =>
            {
                var result = remoteResVersion > GetLocalResVersion();
                onResultGetted(result);
            });
        }

        public void UpdateRes(Action onUpdateDone)
        {
            Debug.Log("开始更新");
            FakeResServer.Instance.DownloadRes(resVersion =>
            {
                ReplaceLocalRes(resVersion);
                Debug.Log("结束更新");
            });
        }

        void ReplaceLocalRes(ResVersion remoteResVersion)
        {
            Debug.Log("2.替换本地资源");
            var localResVersionPath = Application.streamingAssetsPath + "/AssetBundles/Windows/ResVersion.json";
            var remoteResVersionJson = JsonUtility.ToJson(remoteResVersion);
            
            File.WriteAllText(localResVersionPath, remoteResVersionJson);
        }
    }
}