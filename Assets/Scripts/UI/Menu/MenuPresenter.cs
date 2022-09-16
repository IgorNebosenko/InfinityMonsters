using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IM.UI.Menu
{
    public class MenuPresenter
    {
        private MenuView _view;

        public MenuPresenter(MenuView view)
        {
            _view = view;
        }

        public void OnButtonPlayPressed()
        {
            SceneManager.LoadScene(1);
        }

        public void OnButtonNoAdsPressed()
        {
            throw new NotImplementedException();
        }

        public void OnButtonLeaderBoardClicked()
        {
            Social.ShowLeaderboardUI();
        }
    }
}