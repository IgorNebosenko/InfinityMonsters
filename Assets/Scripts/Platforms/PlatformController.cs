﻿using System;
using UnityEngine;
using Zenject;

namespace IM.Platforms
{
    public class PlatformController : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        [SerializeField] private GameObject platformObject;
        [SerializeField] private float liveTimePlatform = 10f;

        private float _timePlatformPassed;
        private IMemoryPool _pool;
        
        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }
        
        public void OnDespawned()
        {
            _pool = null;
            _timePlatformPassed = 0f;
        }

        public void Dispose()
        {
            SetPosition(Vector3.zero);
            _pool?.Despawn(this);
        }

        public PlatformController SetPosition(Vector3 pos)
        {
            platformObject.transform.position = pos;
            return this;
        }

        public class Factory : PlaceholderFactory<PlatformController>
        {
        }
    }
}