using System.Threading.Tasks;
using UnityEngine;

namespace IM.Addressabless
{
    public interface IAddressableResourceProvider
    {
        Task<T> GetAddressableAsync<T>(string path) where T : Object;
    }
}