using System;
using System.Collections.Generic;

namespace Main.Core
{
    /// <summary>
    /// Main dependency container. Is singleton!
    /// </summary>
    public sealed class ServiceLocator
    {
        private static ServiceLocator _inst;
        public static ServiceLocator Inst => _inst;

        private Dictionary<Type, object> _serviceMap;
        private List<IService> _serviceCache;


#if UNITY_EDITOR
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void OnUnityInit()
        {
            UnityEditor.EditorApplication.playModeStateChanged -= ResetStatics;
            UnityEditor.EditorApplication.playModeStateChanged += ResetStatics;
        }
        private static void ResetStatics(UnityEditor.PlayModeStateChange change)
        {
            if(change == UnityEditor.PlayModeStateChange.ExitingEditMode)
            {
                _inst?.Dispose();
                _inst = null;
            }
        }
#endif
        public ServiceLocator()
        {
            if(_inst != null)
            {
                throw new Exception("Error: locator instance is already exists!");
            }
            _inst = this;

            _serviceMap = new Dictionary<Type, object>();
        }
        public void Init()
        {
            foreach (var service in GetAll())
            {
                if (service is IInitableService initable) initable.Init();
            }
        }
        public void Dispose()
        {
            foreach (var service in GetAll())
            {
                if (service is ICleanableService cleanable) cleanable.Clean();
            }
            _serviceMap.Clear();
            _serviceCache.Clear();
        }
        /// <summary>
        /// Register service in ServiceLocator.<br/>
        /// Throws exception if failed to add
        /// </summary>
        public ServiceLocator Register<TService>(TService obj) where TService : class, IService
        {
            Type type = typeof(TService);
            if (_serviceMap.TryAdd(type, obj) == false) 
                throw new Exception($"Error: regestering {type.Name} is failed! It's already exists!");

            return this;
        }
        /// <summary>
        /// Get direct reference from ServiceLocator as generic type.<br/>
        /// Can be null!
        /// </summary>
        public TService Get<TService>() where TService : class, IService
        {
            return _serviceMap[typeof(TService)] as TService;
        }
        /// <summary>
        /// Will try to get service from ServiceLocator as generic type.<br/> 
        /// If it fails return false and null! 
        /// </summary>
        public bool TryGet<TService>(out TService output) where TService : class, IService
        {
            Type type = typeof(TService);

            if (_serviceMap.TryGetValue(type, out object obj) == true)
            {
                output = obj as TService;
                return true;
            }

            output = null;
            return false;
        }
        /// <summary>
        /// Get and cache all services in ServiceLocator.
        /// </summary>
        public List<IService> GetAll()
        {
            _serviceCache ??= new List<IService>();

            foreach (var obj in _serviceMap.Values)
            {
                if (_serviceCache.Contains(obj as IService) == true) continue;

                _serviceCache.Add(obj as IService);
            }

            return _serviceCache;
        }
    }
}