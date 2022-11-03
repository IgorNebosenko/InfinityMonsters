using System;
using UnityEngine;
using Zenject;

namespace IM.Platforms
{
    public class PlatformToken : IPoolable<Vector3, IMemoryPool>, IDisposable
    {
        private PlatformController _platformController;
        private IMemoryPool _pool;

        [Inject] 
        private PlatformController.Factory _platformControllerFactory;
        
        public void OnSpawned(Vector3 pos, IMemoryPool pool)
        {
            _pool = pool;
            _platformController = _platformControllerFactory.Create();
            
            _platformController.SetPosition(pos);
            _platformController.PlatformLifeEnded += OnDespawned;
        }
        
        public void OnDespawned()
        {
            _pool = null;
            _platformController.Dispose();
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<Vector3, PlatformToken>
        {
        }
    }
}