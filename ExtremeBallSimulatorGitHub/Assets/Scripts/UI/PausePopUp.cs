using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class PausePopUp : MonoBehaviour
    {
        [SerializeField]
        private MuteButton muteButton;

        public void ContinueButton()
        {
            GameManager.Instance.PauseButton();
            AudioManager.Instance?.PlayButtonSound();
        }

        public void MuteButton()
        {
            muteButton.PressButton();
        }

        public void ExitButton()
        {
            SceneLoader.Instance.LoadMenu();
            AudioManager.Instance?.PlayButtonSound();
        }
    }
}

