using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QFramework
{
    [Serializable]
    public class ResVersion
    {
        public int Version;

        public List<string> AssetBundleNames = new List<string>();
    }

    public class FakeResServer : MonoSingleton<FakeResServer>
    {
        public static string TempAssetBundlesPath
        {
            get { return Application.persistentDataPath + "/TempAssetBundles/"; }
        }
        
        public void GetRemoteResVersion(Action<int> onRemoteResVersionGet)
        {
            StartCoroutine(HotUpdateMgr.Instance.Config.RequestRemoteResVersion(resVersion => { onRemoteResVersionGet(resVersion.Version); }));
        }

        public void DownloadRes(Action downloadDone)
        {
            StartCoroutine(HotUpdateMgr.Instance.Config.RequestRemoteResVersion(remoteResVersion =>
            {
                StartCoroutine(DoDownloadRes(remoteResVersion, downloadDone));
            }));
        }

        public IEnumerator DoDownloadRes(ResVersion remoteResVersion, Action downloadDone)
        {
            //创建临时目录
            if (!Directory.Exists(TempAssetBundlesPath))
            {
                Directory.CreateDirectory(TempAssetBundlesPath);
            }
            
            //保存 ResVersion.json 文件
            var tempResVersionFilePath = TempAssetBundlesPath + "ResVersion.json";
            var TempResVersionJson = JsonUtility.ToJson(remoteResVersion);
            File.WriteAllText(tempResVersionFilePath, TempResVersionJson);

            var remoteBasePath = HotUpdateMgr.Instance.Config.RemoteAssetBundlesURLBase;
            
            //补上 AssetBundleMenifest 文件比如:Window
            remoteResVersion.AssetBundleNames.Add(ResKitUtil.GetPlatformName());

            foreach (var AssetBundleName in remoteResVersion.AssetBundleNames)
            {
                var www=new WWW(remoteBasePath + AssetBundleName);
                yield return www;

                var bytes = www.bytes;

                var filePath = TempAssetBundlesPath + AssetBundleName;
                File.WriteAllBytes(filePath, bytes);
            }

            downloadDone();
        }
    }
}