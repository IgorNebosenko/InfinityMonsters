using IM.GameData;
using UnityEngine;

namespace IM.Entity
{
    public class BotEntity : BaseEntity
    {
        private int _scoreForDestroy;

        public void SetScoreForDestroy(int minVal, int maxVal)
        {
            _scoreForDestroy = Random.Range(minVal, maxVal);
        }

        public override void Death()
        {
            GameStats.Instance.UpdateCurrentScore(_scoreForDestroy);
            Destroy(gameObject);
        }
    }
}