using System;
using CompanyCoreScripts.Services.PlayerPrefDataPersistenceService;

namespace GameplayScripts.Services.ScoreService
{
    // Current score for this game plus the best score ever, which is saved between sessions
    public class ScoreService : IScoreService
    {
        #region Constants

        private const string BEST_SCORE_KEY = "Line98.BestScore";

        #endregion

        #region Dependencies

        private readonly IPlayerPrefDataPersistenceService persistenceService;

        #endregion

        #region Properties

        public int Score { get; private set; }
        public int BestScore { get; private set; }

        public event Action ScoreChanged;

        #endregion

        #region Constructor

        public ScoreService(IPlayerPrefDataPersistenceService persistenceService)
        {
            this.persistenceService = persistenceService;
            BestScore = persistenceService.Load(BEST_SCORE_KEY, 0);
        }

        #endregion

        #region Public Methods

        public void AddPoints(int points)
        {
            SetScore(Score + points);
        }

        public void SetScore(int score)
        {
            Score = score;
            if (Score > BestScore)
            {
                BestScore = Score;
                persistenceService.Save(BEST_SCORE_KEY, BestScore);
            }

            ScoreChanged?.Invoke();
        }

        public void ResetScore()
        {
            SetScore(0);
        }

        #endregion
    }
}
