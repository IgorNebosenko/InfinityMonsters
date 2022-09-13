using IM.Entity;
using UnityEngine;

namespace IM.Boosts
{
    public class ProjectileForceBoost : BoostBase
    {
        [SerializeField] private float deltaProjectileForce;
        protected override void OnBoostCollected(PlayerEntity player)
        {
            player.UpdateProjectileForce(deltaProjectileForce);
        }
    }
}