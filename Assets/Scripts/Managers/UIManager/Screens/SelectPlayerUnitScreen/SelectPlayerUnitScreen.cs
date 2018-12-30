using System.Linq;
using JetBrains.Annotations;
using Managers.UIManager.Signals;
using Storage;
using UnityEngine;
using UnityEngine.Assertions;
using Utils;
using Zenject;

namespace Managers.UIManager.Screens.SelectPlayerUnitScreen
{
    [Screen(ScreenType.SelectPlayerUnit)]
    public class SelectPlayerUnitScreen : MonoBehaviour
    {
        [SerializeField] 
        private UnitIcon[] _unitIcons;

        [SerializeField] 
        private UnitCharacteristicsView _characteristics;

        private SignalBus _signalBus;
        private UnitSettings _lastSelectedUnit;
        private ISettingsStorage _settingsStorage;

        [Inject]
        public void Construct(SignalBus signalBus, ISettingsStorage settingsStorage)
        {
            _settingsStorage = settingsStorage;
            _signalBus = signalBus;
            _signalBus.Subscribe<PlayerUnitSelectedSignal>(OnUnitSelected);

            InitializeUnitIcons(signalBus, settingsStorage);
            
            OnSelectUnit(settingsStorage.Units.First());
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<PlayerUnitSelectedSignal>(OnUnitSelected);
        }

        private void InitializeUnitIcons(SignalBus signalBus, ISettingsStorage settingsStorage)
        {
            var units = settingsStorage.Units;
            Assert.IsTrue(_unitIcons.Length >= units.Length);

            for (var i = 0; i < _unitIcons.Length; i++)
            {
                var hasUnit = units.Length > i;
                _unitIcons[i].gameObject.SetActive(hasUnit);

                if (hasUnit)
                {
                    _unitIcons[i].Initialize(signalBus, units[i]);
                }
            }
        }
        
        private void OnUnitSelected(PlayerUnitSelectedSignal unitSelectedSignal)
        {
            OnSelectUnit(unitSelectedSignal.Unit);
        }

        private void OnSelectUnit(UnitSettings unit)
        {
            _lastSelectedUnit = unit;
            _characteristics.Initialize(unit);
        }
        
        [UsedImplicitly]
        public void SelectLevel()
        {
            _signalBus.Fire(new OpenScreenSignal
            {
                Type = ScreenType.SelectLevel,
                Context = ContextUtils.Wrap(_lastSelectedUnit, _signalBus, _settingsStorage.Levels),
                HidePrevScreen = true
            });
        }
    }
}