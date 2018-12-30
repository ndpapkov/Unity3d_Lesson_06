using Storage;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Managers.UIManager.Screens.SelectPlayerUnitScreen
{
    public class UnitIcon : MonoBehaviour
    {
        public UnitSettings Unit { get; private set; }

        [SerializeField] 
        private Image _icon;

        private SignalBus _signalBus;

        public void Initialize(SignalBus signalBus, UnitSettings unitSettings)
        {
            Unit = unitSettings;

            _signalBus = signalBus;
            _icon.sprite = Resources.Load<Sprite>(unitSettings.Icon);
        }

        public void OnClick()
        {
            _signalBus.Fire(new PlayerUnitSelectedSignal {Unit = Unit});
        }
    }
}