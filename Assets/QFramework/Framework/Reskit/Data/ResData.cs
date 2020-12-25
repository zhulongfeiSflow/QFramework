using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QFramework
{
    public class ResData : Singleton<ResData>
    {
        private ResData()
        {
            Load();
        }

        public List<AssetBundleData> AssetBundleDatas = new List<AssetBundleData>();
        private static AssetBundleManifest mManifest;

        private void Load()
        {
            if (ResMgr.IsSimulationModeLogic)
            {
#if UNITY_EDITOR
                var assetBundleNames = UnityEditor.AssetDatabase.GetAllAssetBundleNames();

                foreach (var assetBundleName in assetBundleNames)
                {
                    var assetBundleData = new AssetBundleData()
                    {
                        Name = assetBundleName,
                        DependencyBundleNames =
                            UnityEditor.AssetDatabase.GetAssetBundleDependencies(assetBundleName, false),
                    };

                    var assetPaths = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName);

                    foreach (var assetPath in assetPaths)
                    {
                        AssetData assetData = new AssetData()
                        {
                            Name = assetPath.Split('/').Last().Split('.').First(),
                            OwnerBundleName = assetBundleName,
                        };

                        assetBundleData.AssetDataList.Add(assetData);
                    }

                    AssetBundleDatas.Add(assetBundleData);
                }

                AssetBundleDatas.ForEach(abData =>
                {
                    Debug.LogFormat("-------{0}---------", abData.Name);
                    abData.AssetDataList.ForEach(assetData =>
                    {
                        Debug.LogFormat("AB:{0},AssetData:{1}", abData.Name, assetData.Name);
                    });

                    foreach (var dependencyBundleName in abData.DependencyBundleNames)
                    {
                        Debug.LogFormat("AB:{0},Depend:{1}", abData.Name, dependencyBundleName);
                    }
                });
#endif
            }
            else
            {
                var mainBundle =
                    AssetBundle.LoadFromFile(
                        ReskitUtil.FullPathForAssetBundle(ReskitUtil.GetPlatformName()));
                mManifest = mainBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }
        }

        public string[] GetDirectDependencies(string bundleName)
        {
            if (ResMgr.IsSimulationModeLogic)
            {
                return AssetBundleDatas
                    .Find(abData => abData.Name == bundleName)
                    .DependencyBundleNames;
            }

            return mManifest.GetDirectDependencies(bundleName);
        }
    }
}