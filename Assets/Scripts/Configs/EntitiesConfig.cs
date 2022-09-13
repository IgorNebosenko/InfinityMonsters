using System.Linq;
using UnityEngine;

namespace IM.Configs
{
    [CreateAssetMenu(menuName = "Configs/Bots", fileName = "BotsConfig")]
    public class EntitiesConfig : ScriptableObject
    {
        [SerializeField] private BotsData[] botsData;
        [SerializeField] private ScoreSpawnData[] scoreSpawnBotsData;
        [SerializeField] private SpawnBotsPositions positionsConfig;

        [SerializeField] private PlayerData playerData;
        public PlayerData PlayerData => playerData;

        public BotsData[] GetBotsDataForSpawn(int currentScore)
        {
            var countEnemies = 0;

            for (var i = 0; i < scoreSpawnBotsData.Length; i++)
            {
                if (scoreSpawnBotsData[i].requiredScore > currentScore)
                    break;

                countEnemies = scoreSpawnBotsData[i].countItems;
            }

            var positions = positionsConfig.SelectPositions(countEnemies);
            countEnemies = positions.Count;

            var botsResultData = new BotsData[countEnemies];

            var listPossibleBots = botsData.Where(x => x.requiredScore <= currentScore).ToArray();

            for (var i = 0; i < countEnemies; i++)
            {
                var j = i;
                botsResultData[i] = GetBotByWeight(listPossibleBots);
                botsResultData[i].spawnPosition = positions[j];
            }


            return botsResultData;
        }

        private BotsData GetBotByWeight(BotsData[] possibleVariants)
        {
            if (possibleVariants.Length == 0)
                return default;

            if (possibleVariants.Length == 1)
                return possibleVariants[0];

            var requiredWeight = possibleVariants.Sum(x => x.weightForSpawn) * Random.Range(0f, 1f);

            var sumWeight = 0f;
            for (var i = 0; i < possibleVariants.Length; i++)
            {
                if (sumWeight < requiredWeight &&
                    sumWeight + possibleVariants[i].weightForSpawn >= requiredWeight)
                    return possibleVariants[i];

                sumWeight += possibleVariants[i].weightForSpawn;
            }

            return possibleVariants[possibleVariants.Length - 1];
        }

    }
}