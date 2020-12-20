using System;
using System.Collections.Generic;

namespace QFramework
{
    public interface IPool<T>
    {
        T Allocate();
        bool Recycle(T obj);
    }
    public interface IObjectFactory<T>
    {
        T Create();
    }

    public abstract class Pool<T> : IPool<T>
    {
        #region ICountObserverable
        /// <summary>
        /// Gets the current count.
        /// </summary>
        /// <value>The current count.</value>
        public int CurCount {
            get { return mCacheStack.Count; }
        }
        #endregion

        protected IObjectFactory<T> mFactory;
        protected Stack<T> mCacheStack = new Stack<T>();
        /// <summary>
        /// default is 5
        /// </summary>
        protected int mMaxCount = 5;
        public virtual T Allocate() {
            return mCacheStack.Count == 0 ? mFactory.Create() : mCacheStack.Pop();
        }
        public abstract bool Recycle(T obj);
    }

    public class CustomObjectFactory<T> : IObjectFactory<T>
    {
        public CustomObjectFactory(Func<T> factoryMethod) {
            mFactoryMethod = factoryMethod;
        }

        protected Func<T> mFactoryMethod;
        public T Create() {
            return mFactoryMethod();
        }
    }

    /// <summary>
    /// unsafe but fast
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleObjectPool<T> : Pool<T>
    {
        readonly Action<T> mResetMethod;
        public SimpleObjectPool(Func<T> factoryMethod, Action<T> resetMethod = null, int initCount = 0) {
            mFactory = new CustomObjectFactory<T>(factoryMethod);
            mResetMethod = resetMethod;
            for (var i = 0; i < initCount; i++) {
                mCacheStack.Push(mFactory.Create());
            }
        }
        public override bool Recycle(T obj) {
            mResetMethod?.Invoke(obj);
            mCacheStack.Push(obj);
            return true;
        }
    }
}
