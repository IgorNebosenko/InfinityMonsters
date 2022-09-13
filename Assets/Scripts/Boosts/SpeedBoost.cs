using IM.Entity;
using UnityEngine;

namespace IM.Boosts
{
    public class SpeedBoost : BoostBase
    {
        [SerializeField] private float deltaForce;
        protected override void OnBoostCollected(PlayerEntity player)
        {
            player.ChangeSpeed(deltaForce);
        }
    }
}