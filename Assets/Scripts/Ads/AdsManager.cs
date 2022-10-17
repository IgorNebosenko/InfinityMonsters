using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IM.Ads
{
    public class AdsManager : MonoBehaviour
    {
        [SerializeField] private bool enableLogs;
        [SerializeField] private bool testMode;
        
        private List<IAdsProvider> _adsProviders;
        private static AdsManager Instance;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeAdsProviders();
        }

        private void InitializeAdsProviders()
        {
            _adsProviders = new List<IAdsProvider>();
            #if UNITY_ANDROID
            
            _adsProviders.Add(new UnityAndroidAdsProvider());
            
            #endif

            foreach (var adsProvider in _adsProviders)
                adsProvider.Init(enableLogs, testMode);
            
        }

        public static bool TryShowInterstitialAd()
        {
            var providers = Instance._adsProviders.Where(x => x.IsAvailable && x.IsReady).ToArray();
            if (providers.Length == 0)
                return false;
            
            providers[0].ShowInterstitialAd();
            return true;
        }

        public static bool TryShowRewardedAd(Action<AdsCallbackStatus> onComplete)
        {
            var providers = Instance._adsProviders.Where(x => x.IsAvailable && x.IsReady).ToArray();
            if (providers.Length == 0)
                return false;
            
            providers[0].ShowRewardedAd(onComplete);
            return true;
        }
    }
}