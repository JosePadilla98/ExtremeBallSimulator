using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [System.Serializable]
    public class MuteButton
    {
        [SerializeField]
        private Sprite mutedIcon;
        [SerializeField]
        private Sprite unMutedIcon;
        [SerializeField]
        private Image image;

        public void PressButton()
        {
            AudioManager.Instance.PlayButtonSound();
            if (AudioManager.Instance.MuteUnmute())
                image.sprite = unMutedIcon;
            else
                image.sprite = mutedIcon;
        }
    }
}

