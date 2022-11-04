using UnityEngine;

namespace IM.Boosts
{
    public class BoostPoolData
    {
        public BoostBase boostBase;
        public GameObject prefab;
        public Vector3 position;

        public BoostPoolData(BoostBase boostBase, GameObject prefab, Vector3 position)
        {
            this.boostBase = boostBase;
            this.prefab = prefab;
            this.position = position;
        }
    }
}