using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IM.Configs
{
    [CreateAssetMenu(fileName = "SpawnBotsPositions", menuName = "Configs/Spawn Bots Positions")]
    public class SpawnBotsPositions : ScriptableObject
    {
        [SerializeField] private Vector3[] positions;

        public List<Vector3> SelectPositions(int count)
        {
            var result = positions.ToList();
            
            if (count >= positions.Length)
            {
                Debug.LogWarning(
                    "[SpawnBotsPositions] try select positions more than length array! Return all array!");
                return result;
            }

            for (var i = count; i < positions.Length; i++)
            {
                result.RemoveAt(Random.Range(0, result.Count));
            }

            return result;
        }
    }
}