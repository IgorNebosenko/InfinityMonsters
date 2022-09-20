namespace IM.Analytics.Events
{
    public class InterstitialAdViewEvent : AnalyticsEvent
    {
        public override string Key => "interstitial_ad_view";

        public InterstitialAdViewEvent(bool showed)
        {
            Data.Add("showed", showed);
        }
    }
}