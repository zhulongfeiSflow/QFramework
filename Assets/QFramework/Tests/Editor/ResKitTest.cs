using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace QFramework.Tests
{
    public class ResKitTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void LoadAsyncExceptionTest()
        {
            var resLoader = new ResLoader();

            Assert.Throws<Exception>(() =>
            {
                resLoader.LoadAsync<AssetBundle>("square", squareBundle => { });

                resLoader.LoadSync<AssetBundle>("square");
            });

            resLoader.ReleaseAll();
            resLoader = null;

            // Use the Assert class to test conditions
        }

        [UnityTest]
        public IEnumerator LoadAsyncTest()
        {
            var resLoader = new ResLoader();
            
            resLoader.LoadAsync<Texture2D>("resources://BigTexture", bigTexture =>
            {
                Assert.AreEqual("BigTexture", bigTexture.name);
            });

            yield return null;
        }
        
        [UnityTest]
        public IEnumerator LoadAsyncRefCountTest()
        {
            var resLoader = new ResLoader();
            var loadCount = 0;
            
            resLoader.LoadAsync<Texture2D>("resources://BigTexture", bigTexture => { loadCount++; });
            resLoader.LoadAsync<Texture2D>("resources://BigTexture", bigTexture => { loadCount++; });

            yield return new WaitUntil(() =>  loadCount == 2 );

            var bigTextureRes = ResMgr.Instance.SharedLoadedReses.Find(res => res.Name == "resources://BigTexture");
            
            Assert.AreEqual(1, bigTextureRes.RefCount);

            var resLoader2 = new ResLoader();
            resLoader2.LoadSync<Texture2D>("resources://BigTexture");
            
            Assert.AreEqual(2, bigTextureRes.RefCount);
            
            resLoader.ReleaseAll();
            resLoader2.ReleaseAll();
        }

        [UnityTest]
        public IEnumerator LoadABTest()
        {
            var resLoader = new ResLoader();
            resLoader.LoadAsync<AssetBundle>("gameobject",
                bundle =>
                {
                    var gameObjectPrefab = bundle.LoadAsset<GameObject>("GameObject");
                    
                    Assert.IsNotNull(gameObjectPrefab);
                });
            
            yield return null;;
        }
        
    }
}