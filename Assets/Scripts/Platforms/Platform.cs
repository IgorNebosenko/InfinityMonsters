using IM.GameData;
using UnityEngine;
using Zenject;

namespace IM.Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private float platformLiveTime = 10f;

        [SerializeField] private GameObject platform;

        private bool _isUpdateInterrupted;

        private IGameEvents _gameEvents;

        [Inject]
        public void Construct(IGameEvents gameEvents)
        {
            _gameEvents = gameEvents;
            Debug.Log("[DEV] Construct");
        }

        private void OnEnable()
        {
            _gameEvents.OnRespawn += ForceDestroy;
            _gameEvents.OnReset += ForceDestroy;
        }

        private void OnDisable()
        {
            _gameEvents.OnRespawn -= ForceDestroy;
            _gameEvents.OnReset -= ForceDestroy;
        }

        private void Update()
        {
            if (_isUpdateInterrupted)
                return;
            
            platformLiveTime -= Time.deltaTime;
            //TODO: display it time near to platform

            if (platformLiveTime <= 0)
            {
                ForceDestroy();
            }
        }

        private void ForceDestroy()
        {
            Destroy(platform);
            _isUpdateInterrupted = true;
        }
    }
}