using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Managers;
using UnityEngine.UI;

namespace Game.UI
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField]
        private MuteButton muteButton;
        [SerializeField]
        private Text scoreRecordText;

        private void OnEnable()
        {
            string output = PlayerPrefs.GetInt(PlayerPrefsKeys.SCORE_RECORD, 0).ToString();
            output = GeneralUtils.ScoreFormat(output);
            scoreRecordText.text = output;
        }


        public void PlayButton()
        {
            SceneLoader.Instance.LoadGameLevel();
            AudioManager.Instance?.PlayButtonSound();
        }

        public void MuteButton()
        {
            muteButton.PressButton();
        }
    }
}

