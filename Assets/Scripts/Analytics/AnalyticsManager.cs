using System.Collections.Generic;
using UnityEngine;

namespace IM.Analytics
{
    public class AnalyticsManager : MonoBehaviour
    {
        private List<IAnalyticsProvider> _analyticsProviders;
        private static AnalyticsManager Instance;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            InitializeAnalyticsProviders();
        }

        private void InitializeAnalyticsProviders()
        {
            _analyticsProviders = new List<IAnalyticsProvider>();
            
            #if UNITY_ANDROID
            #endif

            foreach (var analyticsProvider in _analyticsProviders)
                analyticsProvider.Init();
        }
    }
}