using UnityEngine;

namespace IM.Addressabless.Pool
{
    public interface IPoolableAddressable
    {
        public string RefId { get; }
        
        public GameObject GameObject { get; }
        
        void OnSpawned(IAddressablePool pool, string id);
        void OnDespawned();
    }
}