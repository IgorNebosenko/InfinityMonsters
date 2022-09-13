using IM.Configs;
using IM.UI.Game;
using UnityEngine;

namespace IM.Aim
{
    public class PlayerGun : MonoBehaviour
    {
        [SerializeField] private Projectile projectileTemplate;
        [SerializeField] private Transform playerTransform;
        
        private float _timePassed;
        
        private GameView _gameView;
        private ProjectileData _projectileData;

        private const float MinCooldownTime = 0.05f;
        private const float MinProjectileMass = 0.1f;

        public void Init(ProjectileData data)
        {
            _projectileData = data;
            _gameView = GameUiReferences.Instance.GameView;
            _gameView.OnButtonShootPressed += TryShoot;
        }

        public void UpdateProjectileMass(float deltaMass)
        {
            _projectileData.mass += deltaMass;

            if (_projectileData.mass < MinProjectileMass)
                _projectileData.mass = MinProjectileMass;
        }

        public void UpdateProjectileForce(float deltaForce)
        {
            _projectileData.projectileForce += deltaForce;
        }

        public void UpdateCooldown(float deltaCooldown)
        {
            _projectileData.cooldown += deltaCooldown;

            if (_projectileData.cooldown < MinCooldownTime)
                _projectileData.cooldown = MinCooldownTime;
        }

        private void OnDestroy()
        {
            _gameView.OnButtonShootPressed -= TryShoot;
        }

        private void FixedUpdate()
        {
            if (_timePassed < _projectileData.cooldown)
                _timePassed += Time.fixedDeltaTime;
            
            _gameView.SetCoolDown(_projectileData.cooldown - _timePassed);
        }

        private void TryShoot()
        {
            if (_timePassed < _projectileData.cooldown)
                return;

            _timePassed = 0;

            var projectile = Instantiate(projectileTemplate, transform.position, Quaternion.identity);
            projectile.SetMass(_projectileData.mass);

            projectile.ProjectileRigidbody.AddForce(playerTransform.forward * _projectileData.projectileForce,
                ForceMode.Impulse);
        }
    }
}