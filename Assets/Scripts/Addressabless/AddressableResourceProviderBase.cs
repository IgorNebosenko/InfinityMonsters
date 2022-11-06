using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace IM.Addressabless
{
    public abstract class AddressableResourceProviderBase : IAddressableResourceProvider
    {
        private ConcurrentDictionary<string, Object> _cashedObjectsAsync = 
            new ConcurrentDictionary<string, Object>();
        
        private BlockingCollection<string> _loadingResourcesAsync = 
            new BlockingCollection<string>();
        
        public async Task<T> GetAddressableAsync<T>(string path) where T : Object
        {
            if (_cashedObjectsAsync.TryGetValue(path, out var obj))
            {
                return (T)obj;
            }

            if (_loadingResourcesAsync.Contains(path))
            {
                while (_loadingResourcesAsync.Contains(path))
                    await Task.Yield();
                return (T)_cashedObjectsAsync[path];
            }

            _loadingResourcesAsync.TryAdd(path);

            var handle = Addressables.LoadAssetAsync<T>(path);
            await handle.Task;
            var result = handle.Result;
            _cashedObjectsAsync.TryAdd(path, result);
            string outs;
            _loadingResourcesAsync.TryTake(out outs);
            return result;
        }
    }
}