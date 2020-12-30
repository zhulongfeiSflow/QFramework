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

        public HotUpdateConfig Config { get; set; }

        private void Awake()
        {
            Config = new HotUpdateConfig();
        }

        public void CheckState(Action done)
        {
            var persistResVersion = Config.LoadHotUpdateAssetBundlesFolderResVersion();

            if (persistResVersion ==  null)
            {
                mState = HotUpdateState.NeverUpdate;
                done();
            }
            else
            {
                StartCoroutine(Config.GetStreamingAssetResVersion(streamingResVersion =>
                {
                    if (persistResVersion.Version > streamingResVersion.Version)
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

        public void GetLocalResVersion(Action<int> onResult)
        {
            if (mState == HotUpdateState.NeverUpdate || mState == HotUpdateState.Overrided)
            {
                StartCoroutine(Config.GetStreamingAssetResVersion(streamingResVersion=>onResult(streamingResVersion.Version)));
                return;
            }
            
            var localResVersion = Config.LoadHotUpdateAssetBundlesFolderResVersion();
            onResult(localResVersion.Version);
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
            FakeResServer.Instance.DownloadRes(() =>
            {
                ReplaceLocalRes();
                Debug.Log("结束更新");
                onUpdateDone();
            });
        }

        void ReplaceLocalRes()
        {
            Debug.Log("2.替换本地资源");
            var tempAssetBundleFolders = FakeResServer.TempAssetBundlesPath;
            var assetBundleFolders = Config.HotUpdateAssetBundlesFolder;

            if (File.Exists(assetBundleFolders))
            {
                Directory.Delete(assetBundleFolders, true);
            }

            Directory.Move(tempAssetBundleFolders, assetBundleFolders);

            if (Directory.Exists(tempAssetBundleFolders))
            {
                Directory.Delete(tempAssetBundleFolders, true);
            }
        }
    }
}