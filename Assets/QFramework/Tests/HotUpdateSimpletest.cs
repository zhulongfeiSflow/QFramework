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
            Application.OpenURL(Application.persistentDataPath);
            
            bool finished = false;
            
            HotUpdateMgr.Instance.CheckState(() =>
            {
                Debug.Log(HotUpdateMgr.Instance.State);
            
                HotUpdateMgr.Instance.HasNewVersionRes(needUpdate =>
                {
                    if (needUpdate)
                    {
                        HotUpdateMgr.Instance.UpdateRes(() =>
                        {
                            Debug.Log("热更结束");
                            Debug.Log("继续");
                            finished = true;
                        });
                    }
                    else
                    {
                        Debug.Log("不需要热更");
                        Debug.Log("继续");
                        finished = true;
                    }
                });
            });

            while (!finished)
            {
                yield return null;
            }
        }
    }
}