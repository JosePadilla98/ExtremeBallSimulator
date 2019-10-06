using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Managers;
using Game.Gameplay;
using TMPro;

namespace Game.UI
{
    public class TopBarHud : MonoBehaviour
    {
        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private Text ballsLeftText;
        [SerializeField]
        private TextMeshProUGUI streakText;

        public GameObject bonusText;

        private void Awake()
        {
            ScoreManager.Instance.ChangeScoreEvent += RefreshScoreText;
            ScoreManager.Instance.ChangeStreakEvent += RefreshStreakText;
            ScoreManager.Instance.ChangeBonusEvent += RefreshBonusText;
            ScoreManager.Instance.ballsBonusEvent += OnBallsBonus;
            LevelController.Instance.BallsNumberChangeEvent += RefreshBallsLeftText;
            bonusText.SetActive(false);
        }

        private void OnDestroy()
        {
            ScoreManager.Instance.ChangeScoreEvent -= RefreshScoreText;
            ScoreManager.Instance.ChangeStreakEvent -= RefreshStreakText;
            ScoreManager.Instance.ChangeBonusEvent -= RefreshBonusText;
            ScoreManager.Instance.ballsBonusEvent -= OnBallsBonus;
            LevelController.Instance.BallsNumberChangeEvent -= RefreshBallsLeftText;
        }

        private void RefreshScoreText()
        {
            string output = ScoreManager.Instance.TotalScore.ToString();
            scoreText.text = GeneralUtils.ScoreFormat(output);
        }

        private void RefreshBallsLeftText()
        {
            ballsLeftText.text = LevelController.Instance.RemainingBallsNum.ToString();
        }

        private void RefreshStreakText()
        {
            string outPut = null;
            int streak = ScoreManager.Instance.Streak;
            if (streak != 0)
                outPut = "Round x" + streak.ToString();

            streakText.text = outPut;
        }

        private void RefreshBonusText(bool value)
        {
            bonusText.SetActive(value);
        }

        private void OnBallsBonus(bool isX2Bonus)
        {
            if (!isX2Bonus)
                streakText.text = "+" + ScoreManager.Instance.scorePerBall.ToString();
            else
                streakText.text = "x2";
        }

        public void PauseButton()
        {
            GameManager.Instance.PauseButton();
            AudioManager.Instance?.PlayButtonSound();
        }
    }
}
