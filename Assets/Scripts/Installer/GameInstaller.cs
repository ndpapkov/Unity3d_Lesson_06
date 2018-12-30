using Managers;
using Managers.UIManager.Screens.SelectPlayerUnitScreen;
using Managers.UIManager.Signals;
using Storage;
using Unit;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] 
        private LocalSettingsStorage _settings;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        
            InstallUnits();
            InstallUIManager();
            InstallGameManager();
        
            Container.Bind<ISettingsStorage>()
                .To<LocalSettingsStorage>()
                .FromInstance(_settings)
                .AsSingle();
        }

        private void InstallUIManager()
        {
            Container.DeclareSignal<CloseScreenSignal>();
            Container.DeclareSignal<OpenScreenSignal>();
            Container.DeclareSignal<BackScreenSignal>();
        }

        private void InstallGameManager()
        {
            Container.Bind<GameManager>().AsSingle();
            Container.DeclareSignal<StartGameSignal>();
        }

        private void InstallUnits()
        {
            Container.BindIFactory<UnitSettings, bool, UnitFacade>()
                .FromFactory<UnitFactory>();
            Container.DeclareSignal<UnitMoveSignal>();
            Container.DeclareSignal<PlayerUnitSelectedSignal>();
            Container.Bind<PlayerUnitController>().AsSingle();
        }
    }
}
