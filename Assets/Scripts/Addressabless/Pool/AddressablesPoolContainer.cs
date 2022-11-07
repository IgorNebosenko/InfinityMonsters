using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IM.Pooling;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace IM.Addressabless.Pool
{
    public class AddressablesPoolContainer : MonoBehaviour
    {
        [SerializeField]
        private List<AssetReferenceT<SubPoolContainer>> listReferences = new List<AssetReferenceT<SubPoolContainer>>();
        [SerializeField] 
        private bool intOnAwake;

        private List<SubPoolContainer> _listSubContainers = new List<SubPoolContainer>();

        [Inject] private Pooling.Pool _pool;

        public async void Fill(bool isSmooth, Action onInit = null)
        {
            var task = new Task(LoadFromReferences);
            task.Start();
            await task;

            var poolDatas = new List<PoolData>();
            var tList = new List<SubPoolContainer>();

            foreach (var item in listReferences)
                tList.Add((SubPoolContainer)item.OperationHandle.Result);

            foreach (var item in tList)
            {
                var data = item.GetData();
                foreach (var poolData in data)
                {
                    if (poolData.sample)
                        poolDatas.Add(new PoolData(poolData.minSize, poolData.maxSize, poolData.sample,
                        poolData.useObjectName ? poolData.sample.name : poolData.name));
                }
                
                _pool.Append(poolDatas.ToArray(), isSmooth, onInit);
                _pool.SetReady();
            }
        }

        private async void LoadFromReferences()
        {
            _listSubContainers.Clear();

            var listOperations = new List<AsyncOperationHandle>();

            for (var i = 0; i < listReferences.Count; i++)
            {
                if (!listReferences[i].IsValid())
                    listOperations.Add(listReferences[i].LoadAssetAsync());
            }

            var groupOperation = Addressables.ResourceManager.CreateGenericGroupOperation(listOperations);
            await groupOperation.Task;
        }
    }
}