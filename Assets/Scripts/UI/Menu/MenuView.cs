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
        [SerializeField] private TMP_Text soundButtonText;
        [SerializeField] private TMP_Text musicButtonText;
        [SerializeField] private Button buttonPlay;
        [SerializeField] private Button buttonNoAds;
        [SerializeField] private Button buttonAchievements;
        [SerializeField] private Button buttonLeaderBoard;
        [SerializeField] private Button buttonSound;
        [SerializeField] private Button buttonMusic;

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

            buttonSound.onClick.AddListener (() => _presenter.ChangeMusicState(SetSoundButtonText));
            buttonMusic.onClick.AddListener(() => _presenter.ChangeSoundState(SetMusicButtonText));
        }

        private void OnDisable()
        {
            buttonPlay.onClick.RemoveListener(_presenter.OnButtonPlayPressed);
            buttonNoAds.onClick.RemoveListener(_presenter.OnButtonNoAdsPressed);
            buttonAchievements.onClick.RemoveListener(_presenter.OnButtonAchievementsClicked);
            buttonLeaderBoard.onClick.RemoveListener(_presenter.OnButtonLeaderBoardClicked);

            buttonSound.onClick.RemoveListener(() => _presenter.ChangeSoundState(SetSoundButtonText));
            buttonMusic.onClick.RemoveListener(() => _presenter.ChangeMusicState(SetMusicButtonText));

        }
        private void SetMusicButtonText(bool state)
        {
            musicButtonText.text = state ? "Music on" : "Music off";
        }
        private void SetSoundButtonText(bool state)
        {
            soundButtonText.text = state ? "Music on" : "Music off";
        }
    }
}
