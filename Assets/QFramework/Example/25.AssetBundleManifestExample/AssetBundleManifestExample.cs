using System.Linq;
using UnityEngine;

namespace QFramework
{
    public class AssetBundleManifestExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/25.AssetBundleManifestExample", false, 25)]
        static void MenuItem()
        {
            var mainAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/StreamingAssets");
            
            var bundleMainifest = mainAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

            bundleMainifest.GetAllDependencies("gameobject")
                .ToList()
                .ForEach(dependency=>{Debug.LogFormat("gameobject dependency:{0}", dependency );});
            
            bundleMainifest.GetAllAssetBundles()
                .ToList()
                .ForEach(assetBundle=>{Debug.Log(assetBundle );});
            
            bundleMainifest.GetDirectDependencies("gameobject")
                .ToList()
                .ForEach(dependency=>{Debug.LogFormat("gameobject dependency:{0}", dependency );});
            
            mainAssetBundle.Unload(true);
        }
        
#endif
    }
}