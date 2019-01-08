using Managers.UIManager.Signals;
using Storage;
using UnityEngine;
using Utils;
using Zenject;

namespace Managers.UIManager.Screens.SelectLevelScreen
{
    [Screen(ScreenType.SelectLevel)]
    public class SelectLevelScreen : MonoBehaviour, IInitializableScreen
    {
        [SerializeField] 
        private LevelItemView _levelItemView;

        private SignalBus _signalBus;
        private LevelSettings[] _levels;
        private UnitSettings _playerUnit;
        private int _currentLevelIndex;

        public void Initialize(object context)
        {
            _signalBus = ContextUtils.Get<SignalBus>(context);
            _levels = ContextUtils.Get<LevelSettings[]>(context);
            _playerUnit = ContextUtils.Get<UnitSettings>(context);
            
            UpdateLevelView();
        }

        private void UpdateLevelView()
        {
            _levelItemView.Initialize(_levels[_currentLevelIndex]);
        }
        
        public void NextLevel()
        {
            _currentLevelIndex = (_currentLevelIndex + 1) % _levels.Length;
            
            UpdateLevelView();
        }

        public void PrevLevel()
        {
            _currentLevelIndex = (_levels.Length + _currentLevelIndex - 1) % _levels.Length;
            
            UpdateLevelView();
        }

        public void StartGame()
        {
            _signalBus.Fire(new CloseScreenSignal { Type = ScreenType.SelectLevel });
            
            _signalBus.Fire(new StartGameSignal
            {
                PlayerUnit = _playerUnit,
                Level = _levels[_currentLevelIndex]
            });
        }
    }
}