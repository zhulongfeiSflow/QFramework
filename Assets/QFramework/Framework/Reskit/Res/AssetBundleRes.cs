using System;
using UnityEngine;

namespace QFramework
{
    public class AssetBundleRes : Res
    {
        private AssetBundle AssetBundle
        {
            get => Asset as AssetBundle;
            set => Asset = value;
        }

        ResLoader mResLoader = new ResLoader();

        public AssetBundleRes(string assetName)
        {
            mPath = ResKitUtil.FullPathForAssetBundle(assetName);

            Name = assetName;

            State = ResState.Waiting;
        }

        public override bool LoadSync()
        {
            State = ResState.Loading;

            var dependencyBundleNames = ResData.Instance.GetDirectDependencies(Name);

            foreach (var dependencyBundleName in dependencyBundleNames)
            {
                mResLoader.LoadSync<AssetBundle>(dependencyBundleName);
            }

            if (!ResMgr.IsSimulationModeLogic)
            {
                AssetBundle = AssetBundle.LoadFromFile(mPath);
            }

            State = ResState.Loaded;

            return AssetBundle;
        }

        public override void LoadAsync()
        {
            State = ResState.Loading;

            LoadDependencyBundlesAsync(() =>
            {
                if (ResMgr.IsSimulationModeLogic)
                {
                    State = ResState.Loaded;
                }
                else
                {
                    var resRequest = AssetBundle.LoadFromFileAsync(mPath);

                    resRequest.completed += operation =>
                    {
                        AssetBundle = resRequest.assetBundle;

                        State = ResState.Loaded;
                    };
                }
            });
        }

        private void LoadDependencyBundlesAsync(Action onAllLoaded)
        {
            var dependencyBundleNames = ResData.Instance.GetDirectDependencies(Name);

            var loadedCount = 0;

            if (dependencyBundleNames.Length == 0)
            {
                onAllLoaded();
            }
            else
            {
                foreach (var dependencyBundleName in dependencyBundleNames)
                {
                    mResLoader.LoadAsync<AssetBundle>(dependencyBundleName,
                        dependencyBundle =>
                        {
                            loadedCount++;

                            if (loadedCount == dependencyBundleNames.Length)
                            {
                                onAllLoaded();
                            }
                        });
                }
            }
        }

        protected override void OnReleaseRes()
        {
            if (AssetBundle != null)
            {
                AssetBundle.Unload(true);
                AssetBundle = null;

                mResLoader.ReleaseAll();
                mResLoader = null;
            }

            if (ResMgr.Instance != null)
            {
                ResMgr.Instance.SharedLoadedReses.Remove(this);
            }
        }
    }
}