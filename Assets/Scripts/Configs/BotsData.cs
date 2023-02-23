using System;
using IM.Entity;
using UnityEngine;

namespace IM.Configs
{
    [Serializable]
    public struct BotsData
    {
        public EntityData entityData;
        public BotEntity botEntityPrefab;

        public float health;

        public int requiredScore;
        public int maxScoreForGenerate;
        
        public float weightForSpawn;

        public int minScoreForDestroy;
        public int maxScoreForDestroy;

        [HideInInspector]
        public Vector3 spawnPosition;
    }
}