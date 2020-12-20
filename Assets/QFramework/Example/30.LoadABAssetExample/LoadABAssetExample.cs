using UnityEngine;

namespace QFramework
{
    public class LoadABAssetExample : MonoBehaviour
    {
        
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/30.LoadABAssetExample", false, 30)]
        static void MenuItem()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("LoadABAssetExample").AddComponent<LoadABAssetExample>();
        }
#endif

        ResLoader mResLoader = new ResLoader();
        
        private void Start()
        {
            var square = mResLoader.LoadSync<Texture2D>("square", "Square.png");
            Debug.Log(square.name);
            
            mResLoader.LoadAsync<GameObject>("gameobject", "GameObject.prefab", prefab =>
            {
                Instantiate(prefab);
            });
        }

        private void OnDestroy()
        {
            // mResLoader.ReleaseAll();
            // mResLoader = null;
        }
    }
}