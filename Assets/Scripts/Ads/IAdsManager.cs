using System;

namespace IM.Ads
{
    public interface IAdsManager
    {
        bool TryShowInterstitialAd();
        bool TryShowRewardedAd(Action<AdsCallbackStatus> onComplete);
    }
}