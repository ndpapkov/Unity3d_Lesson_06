using Managers.UIManager.Signals;
using UnityEngine;
using Zenject;

namespace Managers.UIManager.Screens
{
    [Screen(ScreenType.MainMenu)]
    public class MainMenuScreen : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void StartGame()
        {
            _signalBus.Fire(new OpenScreenSignal(ScreenType.SelectPlayerUnit, true));
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}