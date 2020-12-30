using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace QFramework
{
    public class HotUpdateConfig
    {
        public virtual string HotUpdateAssetBundlesFolder
        {
            get { return Application.persistentDataPath + "/AssetBundles/"; }
        }

        public virtual string LocalResVersionFilepath
        {
            get
            {
                return Application.streamingAssetsPath + "/AssetBundles/" + ResKitUtil.GetPlatformName() +
                       "/ResVersion.json";
            }
        }

        public virtual string LocalAssetBundlesFolder
        {
            get { return Application.streamingAssetsPath + "/AssetBundles/" + ResKitUtil.GetPlatformName() + "/"; }
        }

        public virtual string RemoteResVersionURl
        {
            get { return Application.dataPath + "/QFramework/Framework/Reskit/HotUpdate/Remote/ResVersion.json"; }
        }

        public virtual string RemoteAssetBundlesURLBase
        {
            get { return Application.dataPath + "/QFramework/Framework/Reskit/HotUpdate/Remote/"; }
        }

        public virtual ResVersion LoadHotUpdateAssetBundlesFolderResVersion()
        {
            var persisResVersionFilePath = HotUpdateAssetBundlesFolder + "ResVersion.json";

            if (!File.Exists(persisResVersionFilePath))
            {
                return null;
            }

            var persistResVersionJson = File.ReadAllText(persisResVersionFilePath);
            var persistResVersion = JsonUtility.FromJson<ResVersion>(persistResVersionJson);

            return persistResVersion;
        }

        public virtual IEnumerator GetStreamingAssetResVersion(Action<ResVersion> resVersion)
        {
            var localResVersionFilePath = LocalResVersionFilepath;

            var www = new WWW(localResVersionFilePath);
            yield return www;

            var streamingResVersion = JsonUtility.FromJson<ResVersion>(www.text);
            resVersion(streamingResVersion);
        }

        public virtual IEnumerator RequestRemoteResVersion(Action<ResVersion> onResDownloaded)
        {
            var remoteResVersionPath = RemoteResVersionURl;

            var www = new WWW(remoteResVersionPath);
            yield return www;
            var jsonString = www.text;

            var resVersion = JsonUtility.FromJson<ResVersion>(jsonString);

            onResDownloaded(resVersion);
        }
    }
}