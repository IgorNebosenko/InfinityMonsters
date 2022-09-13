using System;
using IM.Boosts;

namespace IM.Configs
{
    [Serializable]
    public struct BoostData
    {
        public BoostBase boostPrefab;
        public int requiredScore;
        public float percentSpawn;
    }
}