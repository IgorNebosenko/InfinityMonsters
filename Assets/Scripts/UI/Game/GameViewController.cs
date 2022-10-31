using IM.Analytics;
using IM.Analytics.Events;
using UnityEngine;
using Zenject;

namespace IM.UI.Game
{
    public class GameViewController
    {
        private GameView _view;
        private bool _isPauseActive;

        [Inject] private AnalyticsManager _analyticsManager;

        public GameViewController(GameView view)
        {
            _view = view;
        }

        public void OnButtonPauseClicked()
        {
            _isPauseActive = !_isPauseActive;
            _analyticsManager.SendEvent(new GamePauseEvent(_isPauseActive));

            Time.timeScale = _isPauseActive ? 0f : 1f;
        }
    }
}