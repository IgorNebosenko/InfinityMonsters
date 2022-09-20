namespace IM.Analytics.Events
{
    public class BuyNoAdsEvent : AnalyticsEvent
    {
        public override string Key => "buy_no_ads";

        public BuyNoAdsEvent(string buyNoAdsStatus)
        {
            Data.Add("buy_no_ads_status", buyNoAdsStatus);
        }
    }
}