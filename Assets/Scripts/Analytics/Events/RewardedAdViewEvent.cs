namespace IM.Analytics.Events
{
    public class RewardedAdViewEvent : AnalyticsEvent
    {
        public override string Key => "rewarded_ad_view";

        public RewardedAdViewEvent(bool showed, string result)
        {
            Data.Add("showed", showed);
            Data.Add("result", result);
        }
    }
}