using System;
using UnityEngine;

namespace QFramework
{
    public class AssetBundleRes : Res
    {
        private static AssetBundleManifest mManifest;

        private static AssetBundleManifest Manifest
        {
            get
            {
                if (!mManifest)
                {
                    var mainBundle =
                        AssetBundle.LoadFromFile(
                            ReskitUtil.FullPathForAssetBundle(ReskitUtil.GetPlatformName()));
                    mManifest = mainBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                }

                return mManifest;
            }
        }

        private AssetBundle AssetBundle
        {
            get => Asset as AssetBundle;
            set => Asset = value;
        }

        ResLoader mResLoader = new ResLoader();

        public AssetBundleRes(string assetName)
        {
            mPath = ReskitUtil.FullPathForAssetBundle(assetName);

            Name = assetName;

            State = ResState.Waiting;
        }

        public override bool LoadSync()
        {
            State = ResState.Loading;

            // var dependencyBundleNames = Manifest.GetDirectDependencies(Name);
            var dependencyBundleNames = ResData.Instance.GetDirectDependencies(Name);

            foreach (var dependencyBundleName in dependencyBundleNames)
            {
                mResLoader.LoadSync<AssetBundle>(dependencyBundleName);
            }

            // AssetBundle = AssetBundle.LoadFromFile(mPath);

            State = ResState.Loaded;

            return AssetBundle;
        }

        public override void LoadAsync()
        {
            State = ResState.Loading;

            LoadDependencyBundlesAsync(() =>
            {
                var resRequest = AssetBundle.LoadFromFileAsync(mPath);

                resRequest.completed += operation =>
                {
                    AssetBundle = resRequest.assetBundle;

                    State = ResState.Loaded;
                };
            });
        }

        private void LoadDependencyBundlesAsync(Action onAllLoaded)
        {
            var dependencyBundleNames = Manifest.GetDirectDependencies(Name);

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