using System;
using UnityEngine;

namespace IM.Addressabless.Pool
{
    public abstract class PoolableAddressable : MonoBehaviour, IPoolableAddressable, IDisposable
    {
        private IAddressablePool _pool;
        public string RefId { get; private set; }
        public GameObject GameObject => gameObject;
        
        [Serializable]
        public class Reference : ComponentReference<PoolableAddressable>
        {
            public Reference(string guid) : base(guid)
            {
            }
        }
        
        public virtual void OnSpawned(IAddressablePool pool, string id)
        {
            _pool = pool;
            RefId = id;
        }

        public virtual void OnDespawned()
        {
            RefId = String.Empty;
            _pool = null;
        }

        private void OnDestroy()
        {
            RefId = String.Empty;
            _pool = null;
        }

        public void Dispose()
        {
            _pool?.Despawn(this);
        }
    }
}