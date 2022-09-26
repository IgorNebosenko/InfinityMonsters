using IM.GameData;
using UnityEngine.SceneManagement;

namespace IM.UI.Game
{
    public class ErrorShowAdsPresenter
    {
        private ErrorShowAdsPopup _popup;
        
        public ErrorShowAdsPresenter(ErrorShowAdsPopup popup)
        {
            _popup = popup;
        }
        
        public void OnRestartButtonPressed()
        {
            GameStats.Instance.RestartGame();
            _popup.gameObject.SetActive(false);
        }

        public void OnToMenuButtonPressed()
        {
            SceneManager.LoadScene(0);
        }
    }
}