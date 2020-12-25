using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace QFramework
{
    public class NewBehaviourScript
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Playground")]
        static void Test()
        {
            var remoteResVersionPath =
                Application.dataPath + "/QFramework/Framework/Reskit/HotUpdate/RemoteResVersion.json";

            var jsonContent = JsonUtility.ToJson(new ResVersion());
            
            File.WriteAllText(remoteResVersionPath, jsonContent);

            UnityEditor.AssetDatabase.Refresh();
        }

        [Test]
        public void PlayMode()
        {
            Debug.Log(HotUpdateMgr.Instance.GetLocalResVersion());
        }
#endif
    }
}