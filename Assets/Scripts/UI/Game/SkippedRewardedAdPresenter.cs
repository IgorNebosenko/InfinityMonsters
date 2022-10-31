using IM.GameData;
using UnityEngine.SceneManagement;
using Zenject;

namespace IM.UI.Game
{
    public class SkippedRewardedAdPresenter
    {
        private SkippedRewardedAdPopup _popup;

        [Inject] private IGameCore _gameCore;
        
        public SkippedRewardedAdPresenter(SkippedRewardedAdPopup popup)
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