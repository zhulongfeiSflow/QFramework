using UnityEngine;

namespace QFramework
{
    public interface IRefCounter
    {
        int RefCount { get; }

        void Retain(Object refOwner = null);

        void Release(Object refOwner = null);
    }

    public class SimpleRC : IRefCounter
    {
        public int RefCount { get; private set; }

        public void Retain(Object refOwner = null)
        {
            RefCount++;
        }

        public void Release(Object refOwner = null)
        {
            RefCount--;
            if (RefCount == 0)
            {
                OnZeroRef();
            }
        }

        protected virtual void OnZeroRef()
        {
        }
    }
}