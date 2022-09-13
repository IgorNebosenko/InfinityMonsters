using IM.Aim;
using IM.Configs;
using IM.UI.Game;
using UnityEngine;

namespace IM.Entity
{
    public class PlayerEntity : BaseEntity, IHaveTransform
    {
        [SerializeField] private PlayerGun playerGun;

        public void UpdateCooldownSpeed(float deltaCooldown)
        {
            playerGun.UpdateCooldown(deltaCooldown);
        }

        public void UpdateProjectileForce(float deltaProjectileForce)
        {
            playerGun.UpdateProjectileForce(deltaProjectileForce);
        }

        public void UpdateProjectileMass(float deltaProjectileMass)
        {
            playerGun.UpdateProjectileMass(deltaProjectileMass);
        }

        public void SetPlayerData(ProjectileData projectileData)
        {
            playerGun.Init(projectileData);
        }
        
        public override void Death()
        {
            Time.timeScale = 0f;
            GameUiReferences.Instance.RespawnPopup.gameObject.SetActive(true);
        }

        public Transform EntityTransform => transform;
    }
}