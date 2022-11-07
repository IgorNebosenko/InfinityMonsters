using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace IM.Addressabless.Pool
{
    public class AddressablePool<TCom, TRef> : IAddressablePool
        where TCom : IPoolableAddressable
        where TRef : ComponentReference<TCom>
    {
        private Dictionary<string, Stack<TCom>> pool =
            new Dictionary<string, Stack<TCom>>();

        private Dictionary<string, ComponentReference<TCom>> references =
            new Dictionary<string, ComponentReference<TCom>>();

        private readonly GameObject parent;

        public AddressablePool()
        {
            parent = new GameObject($"{typeof(TCom).Name}Pool");
        }

        private async void Register(TRef reference)
        {
            if (!reference.IsValid())
            {
                var load = reference.LoadAssetAsync();
                await load.Task;
            }

            var id = reference.AssetGUID;
            if (pool.ContainsKey(id))
            {
                return;
            }

            references[id] = reference;
            if (!pool.ContainsKey(id))
                pool.Add(id, new Stack<TCom>());
            while (pool[id].Count < 5)
            {
                var task = InstatiateAsync(reference);
                await task;
                pool[id].Push(task.Result);
            }
        }

        private async Task<TCom> InstatiateAsync(TRef reference)
        {
            var operation = reference.InstantiateAsync(parent.transform);
            await operation.Task;
            OnCreated(operation.Result);
            return operation.Result;
        }


        protected void OnCreated(TCom item)
        {
            item.GameObject.SetActive(false);
        }

        protected void OnDestroyed(TCom item)
        {
            GameObject.Destroy(item.GameObject);
        }

        protected void OnSpawned(TCom item, string id)
        {
            item.GameObject.SetActive(true);
            item.OnSpawned(this, id);
        }

        public void Despawn(object item)
        {
            DespawnInternal((TCom) item);
        }

        void IAddressablePool.Register(object item)
        {
            Register((TRef) item);
        }

        public async Task<T> SpawnAsync<T>(object obj) where T : class, IPoolableAddressable
        {
            var reference = (TRef) obj;
            if (!reference.IsValid())
            {
                Debug.LogWarning($"[AddressablePool] reference is null or invalid!");
                return null;
            }

            var id = reference.AssetGUID;

            if (!pool.ContainsKey(id))
                throw new Exception($"[AddressablePool] object with id {id} in pool is not registred!");

            if (pool[id].Count == 0)
            {
                var item = await InstatiateAsync(reference);
                pool[id].Push(item);
            }

            var sItem = pool[id].Pop();
            OnSpawned(sItem, reference.AssetGUID);
            return sItem as T;
        }

        private void DespawnInternal(TCom component)
        {
            var id = component.RefId;
            component.OnDespawned();
            if (component.GameObject.transform.parent != parent.transform)
            {
                component.GameObject.transform.SetParent(parent.transform, false);
            }

            pool[id].Push(component);
        }
    }
}