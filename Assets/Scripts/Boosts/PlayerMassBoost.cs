using IM.Entity;
using UnityEngine;

namespace IM.Boosts
{
    public class PlayerMassBoost : BoostBase
    {
        [SerializeField] private float deltaMass;
        
        protected override void OnBoostCollected(PlayerEntity player)
        {
            player.ChangeMass(deltaMass);
        }
    }
}