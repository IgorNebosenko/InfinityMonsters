using IM.Analytics;
using IM.Analytics.Events;
using IM.Entity;
using UnityEngine;
using Zenject;

namespace IM.Boosts
{
    public class BoostsHandler : MonoBehaviour
    {
        [SerializeField] private PlayerEntity player;
        
        
        [Inject] private IAnalyticsManager _analyticsManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(BoostBase), out var boost))
            {
                _analyticsManager.SendEvent(new CollectBoostEvent(boost.GetType().Name));
                ((BoostBase) boost).BoostCollected(player);
            }
        }
    }
}