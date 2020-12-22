using UnityEngine;

namespace QFramework
{
    public class NewBehaviourScript : MonoBehaviour
    {
        #if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Playground")]
        static void Test()
        {
            var assetBundleNames = UnityEditor.AssetDatabase.GetAllAssetBundleNames();
            foreach (var assetBundleName in assetBundleNames)
            {
                Debug.Log(assetBundleName);

                var assetPaths = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName);

                foreach (var assetPath in assetPaths)
                {
                    Debug.Log(assetPath);
                }
            }
        }
        #endif
    }
}