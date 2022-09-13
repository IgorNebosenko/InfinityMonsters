using System;
using UnityEngine;

namespace IM.Configs
{
    [Serializable]
    public struct ScoreSpawnData
    {
        public int requiredScore;
        [SerializeField] public int countItems;
    }
}