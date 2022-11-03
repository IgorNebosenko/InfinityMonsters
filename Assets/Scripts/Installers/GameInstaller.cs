using IM.Platforms;
using UnityEngine;
using Zenject;

namespace IM.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<Vector3, PlatformToken, PlatformToken.Factory>()
                .FromPoolableMemoryPool(x => 
                    x.WithInitialSize(5));
            Container.BindFactory<PlatformController, PlatformController.Factory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(5)
                    .FromComponentInNewPrefabResource("Platforms/Platform")
                    .UnderTransformGroup("Platforms"));
        }
    }
}