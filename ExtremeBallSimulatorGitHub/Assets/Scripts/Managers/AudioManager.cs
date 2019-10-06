using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Managers
{
    [System.Serializable]
    public class SoundWrapper
    {
        public AudioClip sound;
        public float volumeScale = 1f;
    }

    public class AudioManager : BaseSingleton<AudioManager>
    {
        [SerializeField]
        private AudioSource generalSpeaker;
        [SerializeField]
        private AudioSource collSoundSpeaker;
        [SerializeField]
        private float collSoundPitchVariation = 0.1f;

        //Music
        [SerializeField]
        private AudioClip menuMusic;
        [SerializeField]
        private AudioClip levelsMusic;

        //Sounds effects
        [SerializeField]
        private SoundWrapper propCollision;
        [SerializeField]
        private SoundWrapper propFadeOut;
        [SerializeField]
        private SoundWrapper fxBonus;
        [SerializeField]
        private SoundWrapper buttonSound;
        [SerializeField]
        private SoundWrapper scoreMultiplierSound;

        [SerializeField]
        private AudioMixerSnapshot unpaused;
        [SerializeField]
        private AudioMixerSnapshot paused;

        public bool MuteUnmute()
        {
            generalSpeaker.enabled = !generalSpeaker.enabled;

            if (generalSpeaker.enabled)
                generalSpeaker.Play();

            return generalSpeaker.enabled;
        }

        private void Play()
        {
            if (generalSpeaker.enabled)
                generalSpeaker.Play();
        }

        private void PlayOneShot(SoundWrapper soundWrapper)
        {
            if (generalSpeaker.enabled)
                generalSpeaker.PlayOneShot(soundWrapper.sound, soundWrapper.volumeScale);
        }

        public void SetPausedSnapshot()
        {
            paused.TransitionTo(0.1f);
        }

        public void SetUnpausedSnapshot()
        {
            unpaused.TransitionTo(0.1f);
        }

        #region MUSIC
        public void PlayMenuMusic()
        {
            generalSpeaker.clip = menuMusic;
            Play();
        }

        public void PlayLevelMusic()
        {
            generalSpeaker.clip = levelsMusic;
            Play();
        }
        #endregion

        #region GENERAL EFFECTS
     
        public void PlayPropFadeOut()
        {
            PlayOneShot(propFadeOut);
        }

        public void PlayButtonSound()
        {
            PlayOneShot(buttonSound);
        }

        public void PlayFxBonus()
        {
            PlayOneShot(fxBonus);
        }

        public void PlayScoreMultiplier()
        {
            PlayOneShot(scoreMultiplierSound);
        }
        #endregion

        public void PlayPropCollision()
        {
            if (!generalSpeaker.enabled)
                return;

            collSoundSpeaker.PlayOneShot(propCollision.sound);
            collSoundSpeaker.pitch += collSoundPitchVariation;
        }

        public void ResetPropCollisionPitch()
        {
            collSoundSpeaker.pitch = 1f;
        }
    }
}
