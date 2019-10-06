using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Gameplay;
using Game.Managers;

namespace Game.UI
{
    public class LevelUIController : MonoBehaviour
    {
        [SerializeField]
        private TopBarHud topBar;
        [SerializeField]
        private PausePopUp pausePopPup;
        [SerializeField]
        private EndPopUp endPopUp;

        public void TapInput()
        {
            LevelController.Instance.TapInput();
        }

        private void Awake()
        {
            GameManager.Instance.GameStateChangeEvent += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.Instance.GameStateChangeEvent -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameStates gameState)
        {
            switch (gameState)
            {
                case GameStates.GAMEPLAY_RUNNING:
                    topBar.gameObject.SetActive(true);
                    pausePopPup.gameObject.SetActive(false);
                    endPopUp.gameObject.SetActive(false);
                    break;

                case GameStates.GAMEPLAY_PAUSED:
                    topBar.gameObject.SetActive(false);
                    pausePopPup.gameObject.SetActive(true);
                    endPopUp.gameObject.SetActive(false);
                    break;

                case GameStates.GAME_OVER:
                case GameStates.LEVEL_COMPLETED:
                    topBar.gameObject.SetActive(false);
                    pausePopPup.gameObject.SetActive(false);
                    endPopUp.gameObject.SetActive(true);
                    break;
            }
        }
    }
}
