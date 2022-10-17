using IM.GameData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IM.UI.Menu
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private TMP_Text textHighScore;
        [SerializeField] private TMP_Text textVersion;
        [SerializeField] private Button buttonPlay;
        [SerializeField] private Button buttonNoAds;
        [SerializeField] private Button buttonAchievements;
        [SerializeField] private Button buttonLeaderBoard;

        private MenuPresenter _presenter;

        private void Awake()
        {
            _presenter = new MenuPresenter(this);
        }

        private void OnEnable()
        {
            textHighScore.text = $"High score: {PlayerPrefs.GetInt(GameStats.HighScorePath, 0)}";
            textVersion.text = $"v. {Application.version}";
            
            buttonPlay.onClick.AddListener(_presenter.OnButtonPlayPressed);
            buttonNoAds.onClick.AddListener(_presenter.OnButtonNoAdsPressed);
            buttonAchievements.onClick.AddListener(_presenter.OnButtonAchievementsClicked);
            buttonLeaderBoard.onClick.AddListener(_presenter.OnButtonLeaderBoardClicked);
        }

        private void OnDisable()
        {
            buttonPlay.onClick.RemoveListener(_presenter.OnButtonPlayPressed);
            buttonNoAds.onClick.RemoveListener(_presenter.OnButtonNoAdsPressed);
            buttonAchievements.onClick.RemoveListener(_presenter.OnButtonAchievementsClicked);
            buttonLeaderBoard.onClick.RemoveListener(_presenter.OnButtonLeaderBoardClicked);
        }
    }
}
