using System.Threading.Tasks;

namespace IM.Addressabless.Pool
{
    public interface IAddressablePool
    {
        void Despawn(object item);

        void Register(object item);
        Task<T> SpawnAsync<T>(object item) where T : class, IPoolableAddressable; 
    }
}