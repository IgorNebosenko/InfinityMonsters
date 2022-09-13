using UnityEngine;

namespace IM.UI.Game
{
    public class GameUiReferences : MonoBehaviour
    {
        public GameView GameView;
        public Joystick Joystick;
        public RespawnPopup RespawnPopup;
        
        public static GameUiReferences Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}