using System;
using Zenject;

namespace IM.Boosts
{
    public class BoostToken : IPoolable<BoostPoolData, IMemoryPool>, IDisposable
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

        public class Factory : PlaceholderFactory<BoostPoolData, BoostToken>
        {
        }
    }
}