using System.Collections.Generic;
using System.Linq;
using Managers.UIManager.Signals;
using UnityEngine;
using Zenject;

namespace Managers.UIManager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] 
        private List<Screen> _screenPrefabs;

        private SignalBus _signalBus;
        private readonly List<Screen> _openedScreensStack = new List<Screen>();

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<OpenScreenSignal>(OpenScreen);
            _signalBus.Subscribe<CloseScreenSignal>(CloseScreen);
            _signalBus.Subscribe<BackScreenSignal>(Back);
        }
    
        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OpenScreenSignal>(OpenScreen);
            _signalBus.Unsubscribe<CloseScreenSignal>(CloseScreen);
            _signalBus.Unsubscribe<BackScreenSignal>(Back);
        }

        private void Awake()
        {
            foreach (var screenPrefab in _screenPrefabs)
            {
                screenPrefab.Prefab.SetActive(false);
            }
        }
    
        private void Back(BackScreenSignal backSignal)
        {
            if (_openedScreensStack.Count > 0)
            {
                DisableLastOpened(true);
            }

            if (_openedScreensStack.Count > 0)
            {
                var newLastScreen = _openedScreensStack.Last().Prefab;
                newLastScreen.SetActive(true);
                newLastScreen.transform.SetAsLastSibling();
            }
        }

        private void DisableLastOpened(bool removeFromStack = false)
        {
            if (_openedScreensStack.Count > 0)
            {
                var lastIndex = _openedScreensStack.Count - 1;
                _openedScreensStack[lastIndex].Prefab.SetActive(false);
            
                if (removeFromStack)
                {
                    _openedScreensStack.RemoveAt(lastIndex);
                }
            }
        }

        private void OpenScreen(OpenScreenSignal openScreenSignal)
        {
            if (openScreenSignal.HidePrevScreen && _openedScreensStack.Count > 0)
            {
                DisableLastOpened();
            }
        
            var screen = GetScreen(openScreenSignal.Type);
            var screenPrefab = screen.Prefab;
            screenPrefab.transform.SetAsLastSibling();
            screenPrefab.SetActive(true);

            var initializableScreen = screenPrefab.GetComponent<IInitializableScreen>();
            initializableScreen?.Initialize(openScreenSignal.Context);

            _openedScreensStack.Add(screen);
        }
    
        public void CloseScreen(CloseScreenSignal closeScreenSignal)
        {
            var screen = GetScreen(closeScreenSignal.Type);
            _openedScreensStack.Remove(screen);
            screen.Prefab.SetActive(false);
        }

        private Screen GetScreen(ScreenType type)
        {
            return _screenPrefabs.Find(someScreen => someScreen.Type == type);
        }
    }
}