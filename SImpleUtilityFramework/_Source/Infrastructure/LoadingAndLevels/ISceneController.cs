using System;
using System.Collections.Generic;

namespace Main.Core
{
    /// <summary>
    /// Main scene management interface
    /// </summary>
    public interface ISceneController : IService
    {
        event Action OnSceneLoad;
        event Action OnSceneUnload;


        /// <summary>
        /// Loads scene by name with Single mode<br/>
        /// Throw exception on fail!
        /// </summary>
        void LoadScene(string name, Action onComplete = null);
        /// <summary>
        /// Loads scene by name with Additive mode and set it or not as active<br/>
        /// Throw exception on fail!
        /// </summary>
        void LoadScene(string name, bool isSetActiveScene, Action onComplete = null);
        /// <summary>
        /// Completely unload scene by name<br/>
        /// Throw exception on fail!
        /// </summary>
        void UnloadScene(string name, Action onComplete = null);
        /// <summary>
        /// Get list of all loaded Scenes
        /// </summary>
        List<SceneData> GetCurrentScenes();
    }
}