using System;
using UnityEngine.Advertisements;

namespace IM.Ads
{
    public enum AdsCallbackStatus
    {
        Success,
        Skipped,
        NotAvailable
    }

    public interface IAdsProvider
    {
        bool IsAvailable { get; }
        bool IsReady { get; }

        void Init(bool enableLogs, bool isTestMode);
        
        void ShowInterstitialAd();
        void ShowRewardedAd(Action<AdsCallbackStatus> onComplete);
    }
}