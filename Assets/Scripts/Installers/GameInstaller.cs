using IM.Addressabless.Pool;
using IM.Boosts;
using IM.Configs;
using IM.Platforms;
using IM.Pooling;
using UnityEngine;
using Zenject;

namespace IM.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PoolsConfigs poolsConfigs;
        
        public override void InstallBindings()
        {
            BindAddressablessPools();
            
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
            Container.Bind<AddressablePool<BoostBase, BoostBase.Reference>>()
                .To<AddressablePool<BoostBase, BoostBase.Reference>>()
                .AsSingle().WhenInjectedInto<BoostGenerator>();
        }

        private void BindBulletsPool()
        {
        }

        private void BindBotsPool()
        {
        }

        private void BindAddressablessPools()
        {
            Container.Bind<Pool>().AsSingle().WithArguments(transform).NonLazy();
            Container.Bind<AddressablesPoolContainer>().FromComponentInNewPrefabResource("Pools/GamePool").AsSingle();
        }
    }
}