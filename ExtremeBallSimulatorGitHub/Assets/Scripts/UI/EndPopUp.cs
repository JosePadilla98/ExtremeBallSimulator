using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class EndPopUp : MonoBehaviour
    {
        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private Text titleText;

        public void OnEnable()
        {
            string scoreOutput = ScoreManager.Instance.TotalScore.ToString();
            scoreText.text = GeneralUtils.ScoreFormat(scoreOutput);

            if (GameManager.Instance.GameState == GameStates.LEVEL_COMPLETED)
                titleText.text = "LEVEL COMPLETED";
            else
                titleText.text = "GAME OVER";
        }

        public void RetryButton()
        {
            SceneLoader.Instance.LoadGameLevel();
            AudioManager.Instance?.PlayButtonSound();
        }

        public void ExitButton()
        {
            SceneLoader.Instance.LoadMenu();
            AudioManager.Instance?.PlayButtonSound();
        }
    }
}
