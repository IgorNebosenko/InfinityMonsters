using System;
using IM.GameData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IM.UI.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TMP_Text textHighScore;
        [SerializeField] private TMP_Text textSore;
        [SerializeField] private Button buttonPause;
        [SerializeField] private Button buttonShoot;
        [SerializeField] private TMP_Text cooldownText;

        [SerializeField] private GameStats gameStats;
        
        private GameViewController _controller;

        public event Action OnButtonShootPressed;

        private void Awake()
        {
            _controller = new GameViewController(this);
        }

        private void OnEnable()
        {
            buttonPause.onClick.AddListener(_controller.OnButtonPauseClicked);
            gameStats.OnScoreChanged += SetScoreText;
            buttonShoot.onClick.AddListener(OnButtonShootClicked);
        }

        private void OnDisable()
        {
            buttonPause.onClick.AddListener(_controller.OnButtonPauseClicked);
            gameStats.OnScoreChanged -= SetScoreText;
            buttonShoot.onClick.RemoveListener(OnButtonShootClicked);
        }

        private void Start()
        {
            textHighScore.text = $"Highscore: {GameStats.Instance.HighScore}";
        }

        private void SetScoreText(int val)
        {
            textSore.text = $"Score: {val}";
        }

        private void OnButtonShootClicked()
        {
            OnButtonShootPressed?.Invoke();
        }

        public void SetCoolDown(float value)
        {
            if (value <= 0)
                cooldownText.text = "Ready!";
            else
                cooldownText.text = $"Cooldown : {value:0.0} s";
        }
    }
}
