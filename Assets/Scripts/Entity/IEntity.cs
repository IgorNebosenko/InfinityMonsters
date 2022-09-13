using IM.Configs;
using IM.Entity.Input;

namespace IM.Entity
{
    public interface IEntity
    {
        void Init(EntityData data, IInput input);
        void ChangeSpeed(float val);
        void ChangeMass(float val);
        void Death();
    }
}