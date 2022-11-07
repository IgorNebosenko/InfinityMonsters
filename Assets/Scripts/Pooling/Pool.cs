using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace IM.Pooling
{
    public class Pool
    {
        private readonly Transform _storeLocation;
        private readonly Dictionary<string, BaseObjectPool> _pools;

        private Queue<Task> _fillQueue = new Queue<Task>();
        private Task _currentFill;

        public bool IsReady { get; private set; }

        public Pool(Transform storeLocation)
        {
            _storeLocation = storeLocation;
            _pools = new Dictionary<string, BaseObjectPool>();
        }

        public void Append(PoolData[] poolDatas, bool smoothFill = true, Action onFilled = null)
        {
            var task = new Task(() => Fill(poolDatas, smoothFill, onFilled));

            if (!smoothFill)
            {
                task.Start();
                return;
            }

            if (_currentFill == null)
            {
                _currentFill = task;
                _currentFill.Start();
            }
            else
                _fillQueue.Enqueue(task);
        }

        public T Instantiate<T>(string poolName) where T : class, IPoolObject
        {
            if (!IsReady)
            {
                Debug.LogWarning($"[{GetType().Name}] can't instantiate element with pool name: {poolName}. Pool noy ready!");
                return default;
            }

            var key = poolName;
            if (_pools.TryGetValue(key, out var pool))
                return pool.Instantiate() as T;
            
            Debug.LogError($"[{GetType().Name}] No object pooled with name {poolName} of type {typeof(T)}.");
			
            return default;
        }

        public void Flush()
        {
            foreach (var pool in _pools.Values)
                pool.Clear();
            
            _pools.Clear();
        }

        public void SetReady()
        {
            IsReady = true;
        }

        private async void Fill(PoolData[] poolDatas, bool smoothFill, Action onFilled)
        {
            var start = DateTime.UtcNow;
            foreach (var data in poolDatas)
            {
                if (!data.monoSample)
                {
                    Debug.LogError($"[{GetType().Name}] Sample for key {data.name} is empty");
                }
                else if (!(data.monoSample is IPoolObject))
                {
                    Debug.LogError(
                        $"[{GetType().Name}] Sample for key {data.name} of type {data.monoSample.GetType()} is not derived from {nameof(IPoolObject)}");
                }
                else
                {
                    RegisterGameObjectPool(data.minSize, data.maxSize, data.monoSample, data.name);
                }

                if (smoothFill && (DateTime.UtcNow - start).Milliseconds > 10)
                {
                    await Task.Delay(10);
                    start = DateTime.UtcNow;
                }
            }
            
            onFilled?.Invoke();

            if (_fillQueue.Count > 0)
            {
                _currentFill = _fillQueue.Dequeue();
                
                _currentFill.Start();
            }
            else
                _currentFill = null;
        }
        
        private void RegisterGameObjectPool(int minSize, int maxSize, MonoBehaviour sample, string poolName)
        {	
            var key = poolName;
			
            if(_pools.ContainsKey(key)) return;
			
            var pool = new GameObjectPool(minSize, maxSize, sample, _storeLocation);

            _pools[key] = pool;
        }

    }
}