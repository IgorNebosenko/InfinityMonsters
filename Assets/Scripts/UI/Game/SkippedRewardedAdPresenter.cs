using IM.GameData;
using UnityEngine.SceneManagement;

namespace IM.UI.Game
{
    public class SkippedRewardedAdPresenter
    {
        private SkippedRewardedAdPopup _popup;
        
        public SkippedRewardedAdPresenter(SkippedRewardedAdPopup popup)
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
            GameStats.Instance.UpdateHighScore();
            SceneManager.LoadScene(0);
        }
    }
}