using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace QFramework
{
    public class ResLoader
    {
        #region API

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="assetPath">"resources://"前缀加载Resources目录下的资源,无前缀默认加载AssetBundle资源.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T LoadSync<T>(string assetPath) where T : Object
        {
            return DoLoadSync<T>(assetPath);
        }

        public T LoadSync<T>(string ownerBundleName, string assetName) where T : Object
        {
            return DoLoadSync<T>(assetName, ownerBundleName);
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="assetPath">"resources://"前缀加载Resources目录下的资源,无前缀默认加载AssetBundle资源.</param>
        /// <param name="onLoaded"></param>
        /// <typeparam name="T"></typeparam>
        public void LoadAsync<T>(string assetPath, Action<T> onLoaded) where T : Object
        {
            DoLoadAsync(assetPath, null, onLoaded);
        }

        public void LoadAsync<T>(string ownerBundleName, string assetName, Action<T> onLoaded) where T : Object
        {
            DoLoadAsync(assetName, ownerBundleName, onLoaded);
        }

        public void ReleaseAll()
        {
            mResRecord.ForEach(loadedAsset => { loadedAsset.Release(); });
            mResRecord.Clear();
            mResRecord = null;
        }

        #endregion

        #region private

        private T DoLoadSync<T>(string assetPath, string ownerBundleName = null) where T : Object
        {
            //缓存中查找资源
            var res = GetResFromCache(assetPath);
            if (res != null)
            {
                switch (res.State)
                {
                    case ResState.Waiting:
                    case ResState.Loading:
                        throw new Exception(string.Format("请不要在异步加载资源 {0} 时,进行 {0} 的同步加载.", res.Name));
                    case ResState.Loaded:
                        return res.Asset as T;
                }
            }

            //真正加载资源
            res = CreateRes(assetPath, ownerBundleName);

            res.LoadSync();

            return res.Asset as T;
        }


        private void DoLoadAsync<T>(string assetPath, string ownerBundleName, Action<T> onLoaded) where T : Object
        {
            #region 缓存中查找资源

            var res = GetResFromCache(assetPath);

            Action<Res> onResLoaded = null;
            onResLoaded = loadedRes =>
            {
                onLoaded(loadedRes.Asset as T);
                res.UnRegisterOnLoadedEvent(onResLoaded);
            };

            if (res != null)
            {
                switch (res.State)
                {
                    case ResState.Waiting:
                    case ResState.Loading:
                        res.RegisterOnLoadedEvent(onResLoaded);
                        break;
                    case ResState.Loaded:
                        onLoaded(res.Asset as T);
                        break;
                }

                return;
            }

            #endregion

            //真正加载资源
            res = CreateRes(assetPath, ownerBundleName);

            res.RegisterOnLoadedEvent(onResLoaded);

            res.LoadAsync();
        }

        private List<Res> mResRecord = new List<Res>();

        private Res GetResFromRecord(string assetPath)
        {
            return mResRecord.Find(loadedAsset => loadedAsset.Name == assetPath);
        }

        private Res GetResFromResMgr(string assetPath)
        {
            return ResMgr.Instance.SharedLoadedReses.Find(loadedAsset => loadedAsset.Name == assetPath);
        }

        private void AddRes2Record(Res res)
        {
            mResRecord.Add(res);
            res.Retain();
        }

        private Res CreateRes(string assetName, string ownerBundleName = null)
        {
            Res res = ResFactory.Create(assetName, ownerBundleName);

            ResMgr.Instance.SharedLoadedReses.Add(res);
            
            AddRes2Record(res);
            
            return res;
        }

        private Res GetResFromCache(string assetPath)
        {
            //查询当前资源
            var res = GetResFromRecord(assetPath);
            if (res != null)
            {
                return res;
            }

            //查询全局资源
            res = GetResFromResMgr(assetPath);
            if (res != null)
            {
                AddRes2Record(res);
                return res;
            }

            return null;
        }

        #endregion
    }
}