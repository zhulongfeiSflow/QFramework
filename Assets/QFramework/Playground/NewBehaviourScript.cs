﻿using System.Linq;
using UnityEngine;

namespace QFramework
{
    public class NewBehaviourScript : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Playground")]
        static void Test()
        {
            ResData resData = ResData.Instance;
            resData.AssetBundleDatas.Clear();
            
            var assetBundleNames = UnityEditor.AssetDatabase.GetAllAssetBundleNames();
            
            foreach (var assetBundleName in assetBundleNames)
            {
                var assetBundleData = new AssetBundleData()
                {
                    Name = assetBundleName,
                    DependencyBundleNames = UnityEditor.AssetDatabase.GetAssetBundleDependencies(assetBundleName, false),
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
                
                resData.AssetBundleDatas.Add(assetBundleData);
            }

            resData.AssetBundleDatas.ForEach(abData =>
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
        }
#endif
    }
}