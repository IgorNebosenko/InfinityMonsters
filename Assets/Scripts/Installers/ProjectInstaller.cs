using IM.Ads;
using IM.Analytics;
using IM.GameData;
using IM.GoogleServices;
using UnityEngine;
using Zenject;

namespace IM.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private bool enableLogs = true;

        public override void InstallBindings()
        {
            Application.targetFrameRate = 30;
            
            Container.Bind<AdsManager>().AsSingle().WithArguments(enableLogs, false);
            Container.Bind<IAdsManager>().To<AdsManager>().FromResolve();
            
            Container.Bind<GooglePlayServicesHandler>().AsSingle().WithArguments(enableLogs);
            Container.Bind<IGooglePlayGameServices>().To<GooglePlayServicesHandler>().FromResolve();
            
            Container.Bind<AnalyticsManager>().AsSingle().WithArguments(enableLogs);
            Container.Bind<IAnalyticsManager>().To<AnalyticsManager>().FromResolve();

            Container.Bind<GameStats>().AsSingle();
            Container.Bind<IHighScoreData>().To<GameStats>().FromResolve();
            Container.Bind<IInGameProperties>().To<GameStats>().FromResolve();
            Container.Bind<IGameEvents>().To<GameStats>().FromResolve();
            Container.Bind<IGameCore>().To<GameStats>().FromResolve();
        }
    }
}