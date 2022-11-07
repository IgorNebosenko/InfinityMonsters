using System;
using System.Collections.Generic;
using UnityEngine;

namespace IM.Pooling
{
    [CreateAssetMenu(fileName = "SubPoolContainer", menuName = "Pools/SubPoolContainer")]
    public class SubPoolContainer : ScriptableObject
    {
        [SerializeField] 
        protected List<PoolContainerData> list = new List<PoolContainerData>();

        public List<PoolContainerData> GetData() => list;

        private void Awake()
        {
            CheckLists();
        }

        private void CheckLists()
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].sample is IPoolObject) continue;
                if (list[i].sample)
                    Debug.LogError($"[{GetType().Name}] {list[i].sample.GetType()} of gameobject {list[i].sample.name} dose not contain IPoolObject interface, in pool {name}");
                
                list.RemoveAt(i);
            }
        }
    }
}