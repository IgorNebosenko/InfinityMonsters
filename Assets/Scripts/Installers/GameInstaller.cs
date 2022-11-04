using IM.Boosts;
using IM.Configs;
using IM.Platforms;
using UnityEngine;
using Zenject;

namespace IM.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PoolsConfigs poolsConfigs;
        
        public override void InstallBindings()
        {
            BindPlatformsPool();
            BindBoostsPool();
            BindBulletsPool();
            BindBotsPool();
        }

        private void BindPlatformsPool()
        {
            Container.BindFactory<Vector3, PlatformToken, PlatformToken.Factory>()
                .FromPoolableMemoryPool(x => 
                    x.WithInitialSize(poolsConfigs.platformsInitialPoolSize));
            Container.BindFactory<PlatformController, PlatformController.Factory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(poolsConfigs.platformsInitialPoolSize)
                    .FromComponentInNewPrefabResource("Templates/Platform")
                    .UnderTransformGroup("Platforms"));
        }

        private void BindBoostsPool()
        {
            Container.BindFactory<BoostPoolData, BoostToken, BoostToken.Factory>()
                .FromPoolableMemoryPool(x =>
                    x.WithInitialSize(poolsConfigs.boostsInitialPoolSize));
            Container.BindFactory<BoostPoolData, BoostController, BoostController.Factory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(poolsConfigs.boostsInitialPoolSize)
                    .FromComponentInNewPrefabResource("Templates/Boost")
                    .UnderTransformGroup("Boosts"));
        }

        private void BindBulletsPool()
        {
        }

        private void BindBotsPool()
        {
        }
    }
}