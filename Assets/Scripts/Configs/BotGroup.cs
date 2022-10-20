using System;
using IM.Entity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IM.Configs
{
    [Serializable]
    public class BotGroup
    {
        [SerializeField] private string groupName;

        [SerializeField] private BotEntity[] botPrefabVariants;
        public int requiredScore;
        public float weightForSpawn;
        
        [Space]
        [SerializeField] private float minBotSpeed;
        [SerializeField] private float maxBotSpeed;
        [Space] 
        [SerializeField] private float minBotMass;
        [SerializeField] private float maxBotMass;
        [Space] 
        [SerializeField] private int minBotScore;
        [SerializeField] private int maxBotScore;

        public BotEntity GetRandomBotPrefab()
        {
            return botPrefabVariants.Length == 0 ? null : botPrefabVariants[Random.Range(0, botPrefabVariants.Length)];
        }

        public EntityData GetRandomEntityData()
        {
            return new EntityData()
            {
                speed = Random.Range(minBotSpeed, maxBotSpeed),
                mass = Random.Range(minBotMass, maxBotMass),
                scoreForDestroy = Random.Range(minBotScore, maxBotScore)
            };
        }
    }
}