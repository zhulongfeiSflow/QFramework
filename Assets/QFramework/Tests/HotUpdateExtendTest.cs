using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace QFramework.Tests
{
    public class HotUpdateExtendTest
    {
        public class TestHotUpdateConfig : HotUpdateConfig
        {
            public override string HotUpdateAssetBundlesFolder
            {
                get { return Application.dataPath.Replace("Assets", "HotUpdateAB/"); }
            }
        }

        [Test]
        public IEnumerator HotUpdate_ExpandTest()
        {
            HotUpdateMgr.Instance.Config = new TestHotUpdateConfig();

            HotUpdateMgr.Instance.CheckState(() => { Debug.Log(HotUpdateMgr.Instance.State); });

            return null;
        }
    }
}