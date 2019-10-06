using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Managers
{
    public class SceneLoader : BaseSingleton<SceneLoader>
    {
        //MENU SCENES
        [SerializeField]
        private int mainMenuIndex = 1;
        [SerializeField]
        private int mainMenuSceneIndex = 2;

        //GAMEPLAY UI SCENES
        [SerializeField]
        private int actionPhaseUiIndex = 3;

        //LEVEL SCENES
        public int testLevelIndex = 4;

        private List<int> currentActiveScenes = new List<int>();

        public void LoadMenu()
        {
            StartCoroutine(LoadMenuCor());
        }

        private IEnumerator LoadMenuCor()
        {
            yield return UnloadActiveScenes();

            AsyncOperation asyncLoad1 = SceneManager.LoadSceneAsync(mainMenuIndex, LoadSceneMode.Additive);
            AsyncOperation asyncLoad2 = SceneManager.LoadSceneAsync(mainMenuSceneIndex, LoadSceneMode.Additive);
            while (!asyncLoad1.isDone || !asyncLoad2.isDone)
                yield return null;

            currentActiveScenes.Add(mainMenuIndex);
            currentActiveScenes.Add(mainMenuSceneIndex);
            GameManager.Instance.OnMenuLoaded();
        }

        public void LoadGameLevel()
        {
            StartCoroutine(LoadGameLevelCor(testLevelIndex));
        }

        private IEnumerator LoadGameLevelCor(int levelIndex)
        {
            yield return UnloadActiveScenes();

            //Call the garbage collector to liberate memory
            System.GC.Collect();

            //Load gameplay
            AsyncOperation asyncLoad1 = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Additive);
            AsyncOperation asyncLoad2 = SceneManager.LoadSceneAsync(actionPhaseUiIndex, LoadSceneMode.Additive);
            while (!asyncLoad1.isDone || !asyncLoad2.isDone)
                yield return null;

            currentActiveScenes.Add(levelIndex);
            currentActiveScenes.Add(actionPhaseUiIndex);
            GameManager.Instance.OnLevelLoaded();
        }

        private IEnumerator UnloadActiveScenes()
        {
            for (int i = currentActiveScenes.Count-1 ; i >= 0; i--)
            {
                int sceneIndex = currentActiveScenes[i];

                AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneIndex);
                while (!asyncUnload.isDone)
                    yield return null;

                currentActiveScenes.Remove(sceneIndex);
            }
        }
    }
}
