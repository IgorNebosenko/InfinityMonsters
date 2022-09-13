using System;
using IM.GameData;
using UnityEngine.SceneManagement;

namespace IM.UI.Game
{
    public class RespawnPopupPresenter
    {
        private RespawnPopup _popup;
        public RespawnPopupPresenter(RespawnPopup popup)
        {
            _popup = popup;
        }

        public void OnShowAdsPressed()
        {
            throw new NotImplementedException();
        }

        public void OnSurrenderPressed()
        {
            GameStats.Instance.EndGame();
            SceneManager.LoadScene(0);
        }
    }
}