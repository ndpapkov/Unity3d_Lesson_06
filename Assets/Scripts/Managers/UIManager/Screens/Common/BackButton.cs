using Managers.UIManager.Signals;
using UnityEngine.UI;
using Zenject;

namespace Managers.UIManager.Screens.Common
{
    public class BackButton : Button
    {
        [Inject] 
        private SignalBus _signalBus;

        protected override void Awake()
        {
            base.Awake();
            
            onClick.AddListener(Back);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            onClick.RemoveListener(Back);
        }
        
        private void Back()
        {
            _signalBus.Fire(new BackScreenSignal());
        }
    }
}