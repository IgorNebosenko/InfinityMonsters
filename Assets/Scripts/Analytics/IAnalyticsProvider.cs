namespace IM.Analytics
{
    public interface IAnalyticsProvider
    {
        void Init();
        void SendEvent();
    }
}