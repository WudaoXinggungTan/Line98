using System;

namespace GameplayScripts.Services.ScoreService
{
    public interface IScoreService
    {
        public int Score { get; }
        public int BestScore { get; }

        public event Action ScoreChanged;

        public void AddPoints(int points);
        public void SetScore(int score);
        public void ResetScore();
    }
}
