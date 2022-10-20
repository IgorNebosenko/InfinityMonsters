using UnityEngine;

namespace IM.Entity
{
    public class RandomAbobusColorSelector : MonoBehaviour
    {
        [SerializeField] private Material[] colorsMaterials;
        
        [SerializeField] private Renderer abobusMaterial;

        private void Start()
        {
            abobusMaterial.material = colorsMaterials[Random.Range(0, colorsMaterials.Length)];
        }
    }
}