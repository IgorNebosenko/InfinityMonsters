using IM.GameData;
using Zenject;
using Random = UnityEngine.Random;

namespace IM.Entity
{
    public class BotEntity : BaseEntity
    {
        private int _scoreForDestroy;

        [Inject] private IGameEvents _gameEvents;
        [Inject] private IGameCore _gameCore;

        private void OnEnable()
        {
            _gameEvents.OnReset += DestroyBot;
            _gameEvents.OnRespawn += DestroyBot;
        }

        private void OnDisable()
        {
            _gameEvents.OnReset -= DestroyBot;
            _gameEvents.OnRespawn -= DestroyBot;
        }

        public void SetScoreForDestroy(int minVal, int maxVal)
        {
            _scoreForDestroy = Random.Range(minVal, maxVal);
        }

        public override void Death()
        {
            _gameCore.UpdateCurrentScore(_scoreForDestroy);
            DestroyBot();
        }

        private void DestroyBot()
        {
            Destroy(gameObject);
        }
    }
}