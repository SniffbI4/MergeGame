using System;

namespace Scripts.Pool
{
    public interface IPoolObject
    {
        event Action<IPoolObject> OnObjectNeededToDeactivate;
        void ResetBeforeBackToPool();
    }
}