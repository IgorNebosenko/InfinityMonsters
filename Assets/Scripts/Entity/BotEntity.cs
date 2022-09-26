using IM.GameData;
using Random = UnityEngine.Random;

namespace IM.Entity
{
    public class BotEntity : BaseEntity
    {
        private int _scoreForDestroy;

        private void OnEnable()
        {
            GameStats.Instance.OnReset += DestroyBot;
            GameStats.Instance.OnRespawn += DestroyBot;
        }

        private void OnDisable()
        {
            GameStats.Instance.OnReset -= DestroyBot;
            GameStats.Instance.OnRespawn -= DestroyBot;
        }

        public void SetScoreForDestroy(int minVal, int maxVal)
        {
            _scoreForDestroy = Random.Range(minVal, maxVal);
        }

        public override void Death()
        {
            GameStats.Instance.UpdateCurrentScore(_scoreForDestroy);
            DestroyBot();
        }

        private void DestroyBot()
        {
            Destroy(gameObject);
        }
    }
}