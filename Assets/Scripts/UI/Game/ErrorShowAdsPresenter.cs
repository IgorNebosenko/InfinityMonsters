using IM.GameData;
using UnityEngine.SceneManagement;
using Zenject;

namespace IM.UI.Game
{
    public class ErrorShowAdsPresenter
    {
        private ErrorShowAdsPopup _popup;

        [Inject] private IGameCore _gameCore;
        
        public ErrorShowAdsPresenter(ErrorShowAdsPopup popup)
        {
            _popup = popup;
        }
        
        public void OnRestartButtonPressed()
        {
            _gameCore.RestartGame();
            _popup.gameObject.SetActive(false);
        }

        public void OnToMenuButtonPressed()
        {
            _gameCore.UpdateHighScore();
            SceneManager.LoadScene(0);
        }
    }
}