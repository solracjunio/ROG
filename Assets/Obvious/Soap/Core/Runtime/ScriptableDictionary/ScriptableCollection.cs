using System;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Obvious.Soap
{
    public abstract class ScriptableCollection : ScriptableBase, IReset
    {
        [Tooltip(
            "Clear the collection when:\n" +
            " Scene Loaded : when a scene is loaded.\n" +
            " Application Start : Once, when the application starts. Modifications persist between scenes")]
        [SerializeField] protected ResetType _resetOn = ResetType.SceneLoaded;
        
        [HideInInspector]
        public Action Modified;
        public event Action OnCleared;
        public abstract int Count { get; }
        public abstract bool CanBeSerialized();
        
        protected virtual void Awake()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }

        protected virtual void OnEnable()
        {
            if (_resetOn == ResetType.None)
                return;
            
            Clear();
            if (_resetOn == ResetType.SceneLoaded)
                SceneManager.sceneLoaded += OnSceneLoaded;
            
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

        protected virtual void OnDisable()
        {
            if (_resetOn == ResetType.SceneLoaded)
                SceneManager.sceneLoaded -= OnSceneLoaded;
            
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
        }

        protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //the reset mode can change after the game has started, so we need to check it here.
            if (_resetOn != ResetType.SceneLoaded)
                return;
            
            if (mode == LoadSceneMode.Single)
            {
                Clear();
            }
        }

        public virtual void Clear()
        {
            OnCleared?.Invoke();
            Modified?.Invoke();
        }
        
        internal override void Reset()
        {
            base.Reset();
            _resetOn = ResetType.SceneLoaded;
            Clear();
        }

        public void ResetValue() => Clear();

#if UNITY_EDITOR
        public virtual void OnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == PlayModeStateChange.EnteredEditMode ||
                playModeStateChange == PlayModeStateChange.ExitingEditMode)
                Clear();
        }
#endif
    }
}
