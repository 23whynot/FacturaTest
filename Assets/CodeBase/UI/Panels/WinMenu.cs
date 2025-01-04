using System;
using CodeBase.Services;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Panels
{
    public class WinMenu : MonoBehaviour
    {
        [SerializeField] private Button restartGameButton;
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private ScoreService _scoreService;

        [Inject]
        public void Construct(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        private void OnEnable()
        {
            scoreText.text = "Score: " + _scoreService.GetScore();
        }

        private void Awake()
        {
            restartGameButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            DOTween.KillAll();
        }

        private void OnDestroy()
        {
            restartGameButton.onClick.RemoveAllListeners();
        }
    }
}
