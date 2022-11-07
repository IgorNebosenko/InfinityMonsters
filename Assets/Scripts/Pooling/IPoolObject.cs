using System;

namespace IM.Pooling
{
    public interface IPoolObject
    {
        event Action<IPoolObject> PoolDestroy;


        void Destroy();
		
        void OnCreated();
        void OnDestroyed();
        void OnSpawned();
        void OnDespawned();
    }
}