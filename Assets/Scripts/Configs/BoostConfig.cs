using System.Collections.Generic;
using System.Linq;
using IM.Boosts;
using UnityEngine;

namespace IM.Configs
{
    [CreateAssetMenu(fileName = "BoostsConfig", menuName = "Configs/Boosts")]
    public class BoostConfig : ScriptableObject
    {
        [SerializeField] private BoostData[] boostsData;
        [SerializeField] private ScoreSpawnData[] scoreSpawnBoostsData;

        public List<BoostBase> GetListBoosts(int score)
        {
            var maxBoostsCount = 0;
            var listBoosts = new List<BoostBase>();

            for (var i = 0; i < scoreSpawnBoostsData.Length; i++)
            {
                if (scoreSpawnBoostsData[i].requiredScore > score)
                    break;
                
                maxBoostsCount = scoreSpawnBoostsData[i].countItems;
            }

            if (maxBoostsCount == 0)
                return listBoosts;

            var listAvailableBoosts = boostsData.Where(x => x.requiredScore <= score).ToList();

            if (listAvailableBoosts.Count == 0)
                return listBoosts;

            for (var i = 0; i < maxBoostsCount; i++)
            {
                var index = Random.Range(0, listAvailableBoosts.Count);
                var percentValue = Random.Range(0, 1f);
                
                if (1f - listAvailableBoosts[index].percentSpawn < percentValue)
                    listBoosts.Add(listAvailableBoosts[i].boostPrefab);

            }

            return listBoosts;
        }
    }
}