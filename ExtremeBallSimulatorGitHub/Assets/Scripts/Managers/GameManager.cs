using Game.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public enum GameStates { MENU, GAMEPLAY_RUNNING, GAMEPLAY_PAUSED, LEVEL_COMPLETED, GAME_OVER }

    [System.Serializable]
    public class GameFlowTimes
    {
        public float timeToCountStreak = 0.3f;
        public float timeToCountBonus = 0.3f;
        public float timeBetweenBallsBonus = 0.3f;
        public float timeToLevelCompletePopUp = 0.5f;
        public float timeToGameOverPopUp = 0.3f;
        public float secondsToDisappearEachTypeOfProp = 0.3f;
    }
}

namespace Game.Managers
{
    public class GameManager : BaseSingleton<GameManager>
    {
        public EndRoundParticlesInfo particlesPoolInfo;
        public UnityAction<GameStates> GameStateChangeEvent;
        private GameStates gameState;
        public GameStates GameState { get => gameState; }

        [SerializeField]
        private GameFlowTimes gameFlowTimes;
        public static GameFlowTimes GameFlowTimes { get => Instance.gameFlowTimes; }

        private void Start()
        {
            SceneLoader.Instance.LoadMenu();
        }

        public void OnMenuLoaded()
        {
            AudioManager.Instance.PlayMenuMusic();
            ChangeGameState(GameStates.MENU);
        }

        public void OnLevelLoaded()
        {
            AudioManager.Instance.PlayLevelMusic();
            ScoreManager.Instance.Init();
            LevelController.Instance.Init();
            ChangeGameState(GameStates.GAMEPLAY_RUNNING);
        }

        public void CompleteLevel()
        {
            ScoreManager.Instance.CheckIfRecordReached();
            ChangeGameState(GameStates.LEVEL_COMPLETED);
        }

        public void GameOver()
        {
            ScoreManager.Instance.CheckIfRecordReached();
            ChangeGameState(GameStates.GAME_OVER);
        }

        public void PauseButton()
        {
            #if !UNITY_ANDROID
            if (gameState != GameStates.GAMEPLAY_RUNNING && gameState != GameStates.GAMEPLAY_PAUSED)
                Debug.LogError("This should never happen");
            #endif

            if(gameState == GameStates.GAMEPLAY_RUNNING)
            {

                ChangeGameState(GameStates.GAMEPLAY_PAUSED);
            }
            else
            {
                ChangeGameState(GameStates.GAMEPLAY_RUNNING);
            }
        }

        private void ChangeGameState(GameStates newGameState)
        {
            switch (newGameState)
            {
                case GameStates.MENU:
                    AudioManager.Instance?.SetUnpausedSnapshot();
                    Time.timeScale = 1f;
                    break;

                case GameStates.GAMEPLAY_RUNNING:
                    AudioManager.Instance?.SetUnpausedSnapshot();
                    Time.timeScale = 1f;
                    break;
                    
                case GameStates.GAMEPLAY_PAUSED:
                    AudioManager.Instance?.SetPausedSnapshot();
                    Time.timeScale = 0f;
                    //Call the garbage collector to liberate memory, taking advantage of the occasion by pausing the game
                    System.GC.Collect();
                    break;

                case GameStates.LEVEL_COMPLETED:
                    AudioManager.Instance?.SetPausedSnapshot();
                    Time.timeScale = 0f;
                    System.GC.Collect();
                    break;

                case GameStates.GAME_OVER:
                    AudioManager.Instance?.SetPausedSnapshot();
                    Time.timeScale = 0f;
                    System.GC.Collect();
                    break;
            }

            gameState = newGameState;
            GameStateChangeEvent?.Invoke(gameState);
        }
    }
}
