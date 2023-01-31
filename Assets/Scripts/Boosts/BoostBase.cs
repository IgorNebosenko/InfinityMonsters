using System;
using IM.Addressabless.Pool;
using IM.Entity;

namespace IM.Boosts
{
    public abstract class BoostBase : PoolableAddressable
    {
        [Serializable]
        public class Reference : ComponentReference<BoostBase>
        {
            public Reference(string guid) : base(guid)
            {
            }
        }

        protected abstract void OnBoostCollected(PlayerEntity player);
        
        public void BoostCollected(PlayerEntity player)
        {
            OnBoostCollected(player);
            Destroy(gameObject);
        }
    }
}