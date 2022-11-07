using System;
using UnityEngine;
using Zenject;

namespace IM.Boosts
{
    public class BoostToken : IPoolable<string, Vector3, IMemoryPool>, IDisposable
    {
        public void OnSpawned(string path, Vector3 pos, IMemoryPool pool)
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

        public class Factory : PlaceholderFactory<string, Vector3, BoostToken>
        {
        }
    }
}