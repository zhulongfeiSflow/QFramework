using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace QFramework.Tests
{
    public class HotUpdateTest
    {
        [Test]
        public IEnumerator HotUpdateSimpletest()
        {
            HotUpdateMgr.Instance.HasNewVersionRes(needUpdate =>
            {
                if (needUpdate)
                {
                    HotUpdateMgr.Instance.UpdateRes(() =>
                    {
                        Debug.Log("继续");
                        Assert.IsTrue(true);
                    });
                }
                else
                {
                    Debug.Log("继续");
                    Assert.IsTrue(true);
                }
            });

            yield return null;
        }
    }
}