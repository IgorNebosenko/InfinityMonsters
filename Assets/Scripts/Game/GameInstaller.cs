using UnityEngine;

namespace IM.Game
{
    public class GameInstaller : MonoBehaviour
    {
        private static bool isInited;

        private void Awake()
        {
            if (isInited)
                return;

            Application.targetFrameRate = 30;
            isInited = true;
        }
    }
}
