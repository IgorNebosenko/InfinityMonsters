using System;
using IM.Configs;
using IM.Entity.Input;
using UnityEngine;

namespace IM.Entity
{
    public abstract class BaseEntity : MonoBehaviour, IEntity
    {
        [SerializeField] private Rigidbody physicModel;

        private float speed;
        private float mass;
        private IInput input;

        private const float MinSpeed = 0.5f;
        private const float MinEntityMass = 0.25f;

        protected void FixedUpdate()
        {
            input.OnFixedUpdate(physicModel, speed);
        }

        public void Init(EntityData data, IInput input)
        {
            ChangeSpeed(data.speed);
            ChangeMass(data.mass);
            this.input = input;
        }

        public void ChangeSpeed(float val)
        {
            speed += val;

            if (speed < MinSpeed)
                speed = MinSpeed;
        }

        public void ChangeMass(float val)
        {
            mass += val;

            if (mass < MinEntityMass)
                mass = MinEntityMass;
            
            physicModel.mass = mass;
        }

        public abstract void Death();
    }
}