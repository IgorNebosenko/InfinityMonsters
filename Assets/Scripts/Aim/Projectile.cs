using UnityEngine;

namespace IM.Aim
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody projectileRigidbody;

        public Rigidbody ProjectileRigidbody => projectileRigidbody;
        
        public void SetMass(float mass)
        {
            projectileRigidbody.mass = mass;
        }
    }
}