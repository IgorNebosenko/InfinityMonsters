using IM.Aim;
using IM.Entity;
using UnityEngine;

namespace IM.Platforms
{
    public class DeathZoneCollideHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(BaseEntity), out var entity))
                ((BaseEntity)entity).Death();
            else if (other.TryGetComponent(typeof(Projectile), out var projectile))
                Destroy(projectile.gameObject);
        }
    }
}