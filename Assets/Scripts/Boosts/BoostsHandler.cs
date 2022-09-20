using IM.Analytics;
using IM.Analytics.Events;
using IM.Entity;
using UnityEngine;

namespace IM.Boosts
{
    public class BoostsHandler : MonoBehaviour
    {
        [SerializeField] private PlayerEntity player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(BoostBase), out var boost))
            {
                AnalyticsManager.SendEvent(new CollectBoostEvent(boost.GetType().Name));
                ((BoostBase) boost).BoostCollected(player);
            }
        }
    }
}