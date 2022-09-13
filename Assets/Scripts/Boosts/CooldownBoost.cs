using IM.Entity;
using UnityEngine;

namespace IM.Boosts
{
    public class CooldownBoost : BoostBase
    {
        [SerializeField] private float deltaCooldown;

        protected override void OnBoostCollected(PlayerEntity player)
        {
            player.UpdateCooldownSpeed(deltaCooldown);
        }
    }
}