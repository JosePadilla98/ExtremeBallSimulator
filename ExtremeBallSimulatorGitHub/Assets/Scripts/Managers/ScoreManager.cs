using Game.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Managers
{
    public class ScoreManager : BaseSingleton<ScoreManager>
    {
        public int scorePerBall = 5000;

        private int _totalScore;
        private int roundScore;
        private int _streak;
        private bool isBonusActived;
        public UnityAction<bool> ChangeBonusEvent; //The bool indicates if it is the final x2 bonus
        public UnityAction ChangeScoreEvent;
        public UnityAction ChangeStreakEvent;
        public UnityAction<bool> ballsBonusEvent;
        public int TotalScore { get => _totalScore; set { _totalScore = value; ChangeScoreEvent(); } }
        public int Streak { get => _streak; set { _streak = value; ChangeStreakEvent(); } }

        public void Init()
        {
            TotalScore = 0;
            roundScore = 0;
            Streak = 0;
        }

        public void AddScore(int value)
        {
            TotalScore += value;
            roundScore += value;
            Streak++;
        }

        public void SetBonus(bool value)
        {
            isBonusActived = value;
            ChangeBonusEvent(value);
        }

        public IEnumerator CountRoundScore()
        {
            yield return MultiplyRoundStreak();
            yield return MultiplyBonus();
        }

        private IEnumerator MultiplyRoundStreak()
        {
            if (Streak > 1)
            {
                yield return new WaitForSeconds(GameManager.GameFlowTimes.timeToCountStreak);

                TotalScore -= roundScore;
                roundScore *= _streak;
                TotalScore += roundScore;

                AudioManager.Instance?.PlayScoreMultiplier();
            }
            Streak = 0;
        }

        private IEnumerator MultiplyBonus()
        {
            if (isBonusActived && roundScore > 0)
            {
                yield return new WaitForSeconds(GameManager.GameFlowTimes.timeToCountBonus);

                TotalScore += roundScore;
                AudioManager.Instance?.PlayScoreMultiplier();
            }

            roundScore = 0;
            SetBonus(false);
        }

        public IEnumerator MultiplyRemainingBalls(int ballsLeft)
        {
            for (int i = ballsLeft; i > 0 ; i--)
            {
                yield return new WaitForSeconds(GameManager.GameFlowTimes.timeBetweenBallsBonus);
                TotalScore += scorePerBall;
                LevelController.Instance.RemainingBallsNum--;
                AudioManager.Instance?.PlayScoreMultiplier();
                ballsBonusEvent(false);
            }

            yield return new WaitForSeconds(GameManager.GameFlowTimes.timeBetweenBallsBonus);

            //x2 bonus
            TotalScore *= 2;
            AudioManager.Instance?.PlayScoreMultiplier();
            ballsBonusEvent(true);
        }

        public void CheckIfRecordReached()
        {
            int savedRecord = PlayerPrefs.GetInt(PlayerPrefsKeys.SCORE_RECORD, 0);
            if (savedRecord < TotalScore)
                PlayerPrefs.SetInt(PlayerPrefsKeys.SCORE_RECORD, TotalScore);
        }
    }
}
