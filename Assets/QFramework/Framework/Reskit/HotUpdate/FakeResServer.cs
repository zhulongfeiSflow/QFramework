using System;
using System.Collections;
using System.Collections.Generic;
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
        public void GetRemoteResVersion(Action<int> onRemoteResVersionGet)
        {
            StartCoroutine(RequestRemoteResVersion(resVersion => { onRemoteResVersionGet(resVersion.Version); }));
        }

        public void DownloadRes(Action<ResVersion> onResDownloaded)
        {
            StartCoroutine(RequestRemoteResVersion(onResDownloaded));
        }

        private IEnumerator RequestRemoteResVersion(Action<ResVersion> onResDownloaded)
        {
            var remoteResVersionPath =
                Application.dataPath + "/QFramework/Framework/Reskit/HotUpdate/RemoteResVersion.json";

            var www = new WWW(remoteResVersionPath);
            yield return www;
            var jsonString = www.text;

            var resVersion = JsonUtility.FromJson<ResVersion>(jsonString);

            onResDownloaded(resVersion);
        }
    }
}