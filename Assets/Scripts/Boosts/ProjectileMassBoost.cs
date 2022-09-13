using IM.Entity;
using UnityEngine;

namespace IM.Boosts
{
    public class ProjectileMassBoost : BoostBase
    {
        [SerializeField] private float deltaProjectileMass;
        protected override void OnBoostCollected(PlayerEntity player)
        {
            player.UpdateProjectileMass(deltaProjectileMass);
        }
    }
}