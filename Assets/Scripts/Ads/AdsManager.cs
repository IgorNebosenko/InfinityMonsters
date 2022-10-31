using System;
using System.Collections.Generic;
using System.Linq;

namespace IM.Ads
{
    public class AdsManager : IAdsManager
    {
        private bool _enableLogs;
        private bool _testMode;
        
        private List<IAdsProvider> _adsProviders;

        public AdsManager(bool enableLogs, bool testMode)
        {
            _enableLogs = enableLogs;
            _testMode = testMode;
            
            InitializeAdsProviders();
        }
        
        private void InitializeAdsProviders()
        {
            _adsProviders = new List<IAdsProvider>();
            #if UNITY_ANDROID
            
            _adsProviders.Add(new UnityAndroidAdsProvider());
            
            #endif

            foreach (var adsProvider in _adsProviders)
                adsProvider.Init(_enableLogs, _testMode);
            
        }

        public bool TryShowInterstitialAd()
        {
            var providers = _adsProviders.Where(x => x.IsAvailable).ToArray();
            if (providers.Length == 0)
                return false;
            
            providers[0].ShowInterstitialAd();
            return true;
        }

        public bool TryShowRewardedAd(Action<AdsCallbackStatus> onComplete)
        {
            var providers = _adsProviders.Where(x => x.IsAvailable).ToArray();
            if (providers.Length == 0)
                return false;
            
            providers[0].ShowRewardedAd(onComplete);
            return true;
        }
    }
}