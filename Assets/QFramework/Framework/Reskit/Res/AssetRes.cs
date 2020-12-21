using UnityEngine;

namespace QFramework
{
    public class AssetRes : Res
    {
        private string mOwnerBundleName;

        public AssetRes(string assetName, string ownerBundleName)
        {
            Name = assetName;

            mOwnerBundleName = ownerBundleName;

            State = ResState.Waiting;
        }
        
        ResLoader mResLoader = new ResLoader();
        
        public override bool LoadSync()
        {
            State = ResState.Loading;
            
            var ownerBundle = mResLoader.LoadSync<AssetBundle>(mOwnerBundleName);
            
            Asset = ownerBundle.LoadAsset(Name);
            
            State = ResState.Loaded;
            
            return Asset;
        }

        public override void LoadAsync()
        {
            State = ResState.Loading;
            
            mResLoader.LoadAsync<AssetBundle>(mOwnerBundleName, ownerBundle =>
            {
                var assetBundleRequest = ownerBundle.LoadAssetAsync(Name);

                assetBundleRequest.completed += operation =>
                {
                    Asset = assetBundleRequest.asset;

                    State = ResState.Loaded;
                };
            });
        }

        protected override void OnReleaseRes()
        {
            if (Asset is GameObject)
            {
                
            }
            else
            {
                Resources.UnloadAsset(Asset);
                Asset = null;
            }
            
            mResLoader.ReleaseAll();
            mResLoader = null;

            ResMgr.Instance.SharedLoadedReses.Remove(this);
        }
    }
}