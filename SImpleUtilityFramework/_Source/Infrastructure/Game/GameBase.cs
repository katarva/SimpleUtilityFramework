using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Core
{
    /// <summary>
    /// Main Game base class, state holder and modules repo
    /// </summary>
    public abstract class GameBase
    {
        private ServiceLocator _locator;
        private List<IGameModule> _modules = new List<IGameModule>();
        protected readonly IGameConfig Config;

        /// <summary>
        /// Get current instance of ServiceLocator for internal use
        /// </summary>
        internal ServiceLocator Locator => _locator;
        /// <summary>
        /// Current saved progress of a player
        /// </summary>
        internal virtual IGameProgress CurrentSave { get; set; }


        public GameBase(ServiceLocator locator, IGameConfig config)
        {
            _locator = locator;
            Config = config;
        }
        /// <summary>
        /// Add module to the Game class<br/>
        /// Throws exception if module already exists!
        /// </summary>
        public GameBase AddModule(IGameModule module)
        {
            if (_modules.Contains(module) == true) throw new Exception($"Error: module {module.GetType()} is already exists!");
            _modules.Add(module);

            return this;
        }
        /// <summary>
        /// Initialize all modules and load main menu scene based on IGameConfig object
        /// </summary>
        public void InitGame()
        {
            for (int i = 0; i < _modules.Count; i++)
            {
                _modules[i].Init();
                _modules[i].Subscribe();
            }
            LoadMainMenu();
        }
        /// <summary>
        /// Clean all modules and dispose them
        /// </summary>
        public void CleanUpGame()
        {
            for (int i = 0; i < _modules.Count; i++)
            {
                _modules[i].Unsubscribe();
                _modules[i].Dispose();
            }
            _modules.Clear();
        }
        /// <summary>
        /// Save current progress of the game to the IGameProgressObject variable
        /// </summary>
        public abstract void SetProgress(IGameProgress newSave);
        public abstract void LoadMainMenu();
        public void ExitGame() => Application.Quit();
        /// <summary>
        /// Get concrete module<br/>
        /// Throws exception if nothing found!
        /// </summary>
        public T GetModule<T>() where T : class, IGameModule
        {
            for(int i = 0; i < _modules.Count; i++)
            {
                T temp = _modules[i].IsDesired((x) => x is T) ? _modules[i] as T : null;
                if(temp != null)
                {
                    return temp;
                }
            }

            throw new Exception($"Error: module of type {typeof(T)} is not exists!");
        }
    }
}