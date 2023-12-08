using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Core
{
    /// <summary>
    /// Main loader for ISceneController classes that controls it<br/>
    /// Needs a ICoroutineSource Mono object!
    /// </summary>
    public class Loader
    {
        private readonly ICoroutineSource _coroutineSource;


        public Loader(ICoroutineSource source)
        {
            _coroutineSource = source;
        }
        public void LoadScene(string name, bool isSetActiveScene, LoadSceneMode mode, Action<bool> isSucceed)
        {
            _coroutineSource.StartCoroutine(LoadSceneCoroutine(name, isSetActiveScene, mode, isSucceed));
        }
        public void UnloadScene(string name, Action<bool> isSucceed)
        {
            _coroutineSource.StartCoroutine(UnloadSceneCoroutine(name, isSucceed));
        }
        private IEnumerator LoadSceneCoroutine(string name, bool isSetActiveScene, LoadSceneMode mode, Action<bool> isSucceed)
        {
            if (SceneManager.GetActiveScene().name == name || SceneManager.GetSceneByName(name).isLoaded == true)
            {
                isSucceed?.Invoke(false);
                
                yield break;
            }

            AsyncOperation loading = SceneManager.LoadSceneAsync(name, mode);

            while (loading.isDone == false)
            {
                yield return null;
            }

            if (isSetActiveScene == true)
            {
                Scene scene = SceneManager.GetSceneByName(name);
                SceneManager.SetActiveScene(scene);
            }
            isSucceed?.Invoke(true);
        }
        private IEnumerator UnloadSceneCoroutine(string name, Action<bool> isSucceed)
        {
            if (SceneManager.GetSceneByName(name).isLoaded == false)
            {
                isSucceed?.Invoke(false);
                yield break;
            }

            AsyncOperation unloading = SceneManager.UnloadSceneAsync(name);

            while (unloading.isDone == false)
            {
                yield return null;
            }
            isSucceed?.Invoke(true);
        }
    }
}