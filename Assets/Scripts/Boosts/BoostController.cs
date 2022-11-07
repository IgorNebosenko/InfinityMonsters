using System;
using UnityEngine;
using Zenject;

namespace IM.Boosts
{
    public class BoostController : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {

        public void OnSpawned(IMemoryPool pool)
        {
        }
        
        public void OnDespawned()
        {
        }

        public void Dispose()
        {
            
        }

        public BoostController SetPosition(Vector3 pos)
        {
            transform.position = pos;
            return this;
        }
        
        
        
        public class Factory : PlaceholderFactory<BoostController>
        {
        }
    }
}