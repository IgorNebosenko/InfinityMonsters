using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace IM.Ads
{
    public class UnityAndroidAdsProvider : IAdsProvider, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private bool _enableLogs;

        private const string ProjectId = "4935570";
        private const string InterstitialAdId = "Interstitial_Android";
        private const string RewardedAdId = "Rewarded_Android";

        private Action<AdsCallbackStatus> onFinishRewardedVideo;
        
        public bool IsAvailable { get; private set; }

        public void Init(bool enableLogs, bool isTestMode)
        {
            _enableLogs = enableLogs;
            Advertisement.Initialize(ProjectId, isTestMode, true, this);
            LoadAds();
        }

        private void LoadAds()
        {
            Advertisement.Load(InterstitialAdId, this);
            Advertisement.Load(RewardedAdId, this);
        }

        public void ShowInterstitialAd()
        {
            Advertisement.Show(InterstitialAdId, this);
        }

        public void ShowRewardedAd(Action<AdsCallbackStatus> onComplete)
        {
            onFinishRewardedVideo = onComplete;
            Advertisement.Show(RewardedAdId, this);
        }

        public void OnInitializationComplete()
        {
            if (_enableLogs)
                Debug.Log("[UnityAndroidAdsProvider] Initialize completed!");
            
            IsAvailable = true;
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            if (_enableLogs)
                Debug.LogError($"[UnityAndroidAdsProvider] Initialize error! Message: {message}");
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            if (_enableLogs)
                Debug.Log($"[UnityAndroidAdsProvider] load ad with id {placementId}");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            if (_enableLogs)
                Debug.Log($"[UnityAndroidAdsProvider] error load ad with id {placementId}. Message: {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            if (_enableLogs)
                Debug.Log($"[UnityAndroidAdsProvider] error show ad with id: {placementId}. Message: {message}");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            if (_enableLogs)
                Debug.Log($"[UnityAndroidAdsProvider] start showing ad with id {placementId}");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            if (_enableLogs)
                Debug.Log($"[UnityAndroidAdsProvider] load ad with id {placementId}");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (_enableLogs)
                Debug.Log($"[UnityAndroidAdsProvider] show ad complete with id {placementId}");

            if (string.Equals(placementId, RewardedAdId))
            {
                switch (showCompletionState)
                {
                    case UnityAdsShowCompletionState.SKIPPED:
                        onFinishRewardedVideo?.Invoke(AdsCallbackStatus.Skipped);
                        break;
                    
                    case UnityAdsShowCompletionState.UNKNOWN:
                        onFinishRewardedVideo?.Invoke(AdsCallbackStatus.NotAvailable);
                        break;
                    
                    case UnityAdsShowCompletionState.COMPLETED:
                        onFinishRewardedVideo?.Invoke(AdsCallbackStatus.Success);
                        break;
                }

                onFinishRewardedVideo = null;
            }
        }
    }
}