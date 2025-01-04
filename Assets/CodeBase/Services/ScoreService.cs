using System;

namespace CodeBase.Services
{
    public class ScoreService 
    {
        private int _scoreCount;
        
        public Action<int> OnScoreChanged;
        
        public void IncrementScore(int count)
        {
            _scoreCount += count;
            OnScoreChanged?.Invoke(_scoreCount);
        }

        public int GetScore()
        {
            return _scoreCount;
        }
    }
}
