using IM.Entity;
using UnityEngine;

namespace IM.Boosts
{
    public abstract class BoostBase : MonoBehaviour
    {
        protected abstract void OnBoostCollected(PlayerEntity player);
        
        public void BoostCollected(PlayerEntity player)
        {
            OnBoostCollected(player);
            Destroy(gameObject);
        }
    }
}