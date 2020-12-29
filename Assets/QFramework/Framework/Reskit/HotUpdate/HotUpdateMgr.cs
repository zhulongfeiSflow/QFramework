using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace QFramework
{
    public enum HotUpdateState
    {
        /// <summary>
        /// 从未跟新过
        /// </summary>
        NeverUpdate,

        /// <summary>
        /// 更新过
        /// </summary>
        Updated,

        /// <summary>
        /// 覆盖安装过,有旧的的资源
        /// </summary>
        Overrided,
    }

    public class HotUpdateMgr : MonoSingleton<HotUpdateMgr>
    {
        private HotUpdateState mState;

        public HotUpdateState State
        {
            get => mState;
        }

        public void CheckState(Action done)
        {
            var persistResVersionFilePath = ResKitUtil.PersistentAssetBundlesFolder + "ResVersion.json";

            if (!File.Exists(persistResVersionFilePath))
            {
                mState = HotUpdateState.NeverUpdate;
                done();
            }
            else
            {
                var persistResVersionJson = File.ReadAllText(persistResVersionFilePath);
                var persistResVersion = JsonUtility.FromJson<ResVersion>(persistResVersionJson);

                StartCoroutine(GetStreamingAssetResVersion(streamingResVersion =>
                {
                    if (persistResVersion.Version > streamingResVersion)
                    {
                        mState = HotUpdateState.Updated;
                    }
                    else
                    {
                        mState = HotUpdateState.Overrided;
                    }

                    done();
                }));
            }
        }

        IEnumerator GetStreamingAssetResVersion(Action<int> getResVersion)
        {
            var streamingResVersionFilePath = Application.streamingAssetsPath + "/AssetBundles/Windows/ResVersion.json";
            var www = new WWW(streamingResVersionFilePath);
            yield return www;

            var resVersion = JsonUtility.FromJson<ResVersion>(www.text);
            getResVersion(resVersion.Version);
        }

        public void GetLocalResVersion(Action<int> getLocalResVersion)
        {
            if (mState == HotUpdateState.NeverUpdate || mState == HotUpdateState.Overrided)
            {
                StartCoroutine(GetStreamingAssetResVersion(streamingResVersion =>
                {
                    getLocalResVersion(streamingResVersion);
                }));
                return;
            }

            var localResVersionPath = ResKitUtil.PersistentAssetBundlesFolder + "ResVersion.json";
            var jsonString = File.ReadAllText(localResVersionPath);
            var localResVersion = JsonUtility.FromJson<ResVersion>(jsonString);
            getLocalResVersion(localResVersion.Version);
        }

        public void HasNewVersionRes(Action<bool> onResultGetted)
        {
            FakeResServer.Instance.GetRemoteResVersion(remoteResVersion =>
            {
                GetLocalResVersion(localResVersion =>
                {
                    var result = remoteResVersion > localResVersion;
                    onResultGetted(result);
                });
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
            var localResVersionPath = ResKitUtil.PersistentAssetBundlesFolder + "ResVersion.json";
            var remoteResVersionJson = JsonUtility.ToJson(remoteResVersion);

            if (!File.Exists(ResKitUtil.PersistentAssetBundlesFolder))
            {
                Directory.CreateDirectory(ResKitUtil.PersistentAssetBundlesFolder);
            }

            File.WriteAllText(localResVersionPath, remoteResVersionJson);
        }
    }
}