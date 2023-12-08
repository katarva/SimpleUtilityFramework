using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Core
{
    public class SceneController : ISceneController
    {
        public event Action OnSceneLoad;
        public event Action OnSceneUnload;

        private readonly Loader _loader;

        
        public SceneController(ICoroutineSource source)
        {
            _loader = new Loader(source);
        }
        public void LoadScene(string name, Action onComplete = null)
        {
            _loader.LoadScene(name, false, LoadSceneMode.Single, (isSucceed) =>
            {
                if (isSucceed == true)
                {
                    OnSceneLoad?.Invoke();
                    onComplete?.Invoke();
                }
                else
                {
                    throw new Exception($"Error: Loading {name} failed!");
                }
            });
        }
        public void LoadScene(string name, bool isSetActiveScene, Action onComplete = null)
        {
            _loader.LoadScene(name, isSetActiveScene, LoadSceneMode.Additive, (isSucceed) =>
            {
                if (isSucceed == true)
                {
                    OnSceneLoad?.Invoke();
                    onComplete?.Invoke();
                }
                else
                {
                    throw new Exception($"Error: Loading {name} failed!");
                }
            });
        }
        public void UnloadScene(string name, Action onComplete = null)
        {
             _loader.UnloadScene(name, (isSucceed) =>
             {
                 if (isSucceed == true)
                 {
                     OnSceneUnload?.Invoke();
                     onComplete?.Invoke();
                 }
                 else
                 {
                     throw new Exception($"Error: Unloading {name} failed!");
                 }
             });
        }
        public List<SceneData> GetCurrentScenes()
        {
            List<SceneData> result = new List<SceneData>();

            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                result.Add(new SceneData
                {
                    Name = scene.name,
                    IsLoaded = scene.isLoaded,
                    IsActive = SceneManager.GetActiveScene().buildIndex == scene.buildIndex
                });
            }

            return result;
        }
    }
}
