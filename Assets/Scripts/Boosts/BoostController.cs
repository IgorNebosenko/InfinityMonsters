using System;
using UnityEngine;
using Zenject;

namespace IM.Boosts
{
    public class BoostController : MonoBehaviour, IPoolable<BoostPoolData, IMemoryPool>, IDisposable
    {
        public void OnSpawned(BoostPoolData data, IMemoryPool pool)
        {
            throw new NotImplementedException();
        }
        
        public void OnDespawned()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        
        public class Factory : PlaceholderFactory<BoostPoolData, BoostController>
        {
        }
    }
}