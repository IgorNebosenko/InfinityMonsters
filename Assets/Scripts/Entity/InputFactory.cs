using IM.Entity.Input;

namespace IM.Entity
{
    public class InputFactory
    {
        public IInput GetPlayerInput()
        {
            return new PlayerInput();
        }

        public IInput GetBotInput(IHaveTransform playerPos)
        {
            return new BotInput(playerPos);
        }
    }
}