using System;
using UnityEngine;

namespace QFramework.Example._26.LoadAsyncTest1
{
    public class LoadAsyncTest1 : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/26.LoadAsyncTest1", false, 26)]
        static void MenuItem()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("LoadAsyncTest1")
                .AddComponent<LoadAsyncTest1>();
        }
        
#endif

        ResLoader mResLoader = new ResLoader();
        private void Start()
        {
            mResLoader.LoadAsync<AssetBundle>("square", squareBundle =>
            {
                Debug.Log((squareBundle.name));
            });

            mResLoader.LoadSync<AssetBundle>("square");
        }
    }
}