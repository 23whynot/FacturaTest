﻿using CodeBase.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.Core
{
    public class ScoreVisual : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        [Inject]
        public void Construct(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        private ScoreService _scoreService;

        private void Start()
        {
            _scoreService.OnScoreChanged += ShowScore;
        }

        private void ShowScore(int count)
        {
            scoreText.text = count.ToString();
        }

        private void OnDestroy()
        {
            _scoreService.OnScoreChanged -= ShowScore;
        }
    }
}