using IM.Analytics.Events;

namespace IM.Analytics
{
    public interface IAnalyticsProvider
    {
        bool Ready { get; }
        void Init(bool enableLogs);
        void SendEvent(AnalyticsEvent analyticsEvent);
    }
}