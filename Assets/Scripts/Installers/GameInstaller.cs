using IM.Platforms;
using UnityEngine;
using Zenject;

namespace IM.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private int sizePlatformsPool;
        
        public override void InstallBindings()
        {
            Container.BindFactory<Vector3, PlatformToken, PlatformToken.Factory>()
                .FromPoolableMemoryPool(x => 
                    x.WithInitialSize(sizePlatformsPool));
            Container.BindFactory<PlatformController, PlatformController.Factory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(sizePlatformsPool)
                    .FromComponentInNewPrefabResource("Platforms/Platform")
                    .UnderTransformGroup("Platforms"));
        }
    }
}