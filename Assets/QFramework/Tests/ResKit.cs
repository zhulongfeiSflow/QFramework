using System;
// using NUnit.Framework;
using UnityEngine;
using UnityEngine.Assertions;

namespace QFramework.Tests
{
    public class ResKit
    {
        // [Test]
        public void LoadAsyncExceptionTest()
        {
            var resLoader = new ResLoader();

            // Assert.Throws<Exception>(() =>
            // {
            //     resLoader.LoadAsync<AssetBundle>("square", squareBundle =>
            //     {
            //         Debug.Log((squareBundle.name));
            //     });
            //
            //     resLoader.LoadSync<AssetBundle>("square");
            // });

            resLoader.ReleaseAll();
            resLoader = null;
        }
    }
}