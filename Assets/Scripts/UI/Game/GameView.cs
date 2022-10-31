using System;
using IM.GameData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace IM.UI.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TMP_Text textHighScore;
        [SerializeField] private TMP_Text textSore;
        [SerializeField] private Button buttonPause;
        [SerializeField] private ButtonShoot buttonShoot;

        [Inject] private IGameEvents _gameEvents;
        [Inject] private IHighScoreData _highScoreData;
        
        private GameViewController _controller;

        public event Action OnButtonShootPressed;

        private void Awake()
        {
            _controller = new GameViewController(this);
        }

        private void OnEnable()
        {
            buttonPause.onClick.AddListener(_controller.OnButtonPauseClicked);
            _gameEvents.OnScoreChanged += SetScoreText;
            buttonShoot.OnHoldButton += OnButtonShootHolded;
            _gameEvents.OnHighScoreUpdated += HighScoreTextUpdate;
        }

        private void OnDisable()
        {
            buttonPause.onClick.AddListener(_controller.OnButtonPauseClicked);
            _gameEvents.OnScoreChanged -= SetScoreText;
            buttonShoot.OnHoldButton -= OnButtonShootHolded;
            _gameEvents.OnHighScoreUpdated -= HighScoreTextUpdate;
        }

        private void Start()
        {
            HighScoreTextUpdate(_highScoreData.HighScore);
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
