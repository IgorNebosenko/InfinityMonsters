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
        [SerializeField] private ButtonShoot buttonShoot;

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
            buttonShoot.OnHoldButton += OnButtonShootHolded;
            gameStats.OnHighScoreUpdated += HighScoreTextUpdate;
        }

        private void OnDisable()
        {
            buttonPause.onClick.AddListener(_controller.OnButtonPauseClicked);
            gameStats.OnScoreChanged -= SetScoreText;
            buttonShoot.OnHoldButton -= OnButtonShootHolded;
            gameStats.OnHighScoreUpdated -= HighScoreTextUpdate;
        }

        private void Start()
        {
            HighScoreTextUpdate(GameStats.Instance.HighScore);
        }

        private void HighScoreTextUpdate(int val)
        {
            textHighScore.text = $"Highscore: {val}";
        }

        private void SetScoreText(int val)
        {
            textSore.text = $"Score: {val}";
        }

        private void OnButtonShootHolded()
        {
            OnButtonShootPressed?.Invoke();
        }
    }
}
