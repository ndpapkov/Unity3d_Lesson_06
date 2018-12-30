using Managers.UIManager;
using Managers.UIManager.Signals;
using Storage;
using Unit;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private IFactory<UnitSettings, bool, UnitFacade> _unitFactory;
        private ISettingsStorage _settingsStorage;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(
            SignalBus signalBus,
            IFactory<UnitSettings, bool, UnitFacade> unitFactory,
            ISettingsStorage storage)
        {
            _signalBus = signalBus;
            _settingsStorage = storage;
            _unitFactory = unitFactory;

            _signalBus.Subscribe<StartGameSignal>(StartGame);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<StartGameSignal>(StartGame);
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            _signalBus.Fire(new OpenScreenSignal(ScreenType.MainMenu, true));
        }

        private void StartGame(StartGameSignal startGame)
        {
            _unitFactory.Create(startGame.PlayerUnit, true);

            foreach (var enemyUnit in startGame.Level.EnemyUnits)
            {
                var unitSettings = _settingsStorage.GetUnitById(enemyUnit.UnitId);
                var unit = _unitFactory.Create(unitSettings, false);
                unit.transform.position = enemyUnit.Position;
            }
        }
    }
}