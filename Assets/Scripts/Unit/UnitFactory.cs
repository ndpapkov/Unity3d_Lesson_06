using Storage;
using Zenject;

namespace Unit
{
    public class UnitFactory : IFactory<UnitSettings, bool, UnitFacade>
    {
        private readonly DiContainer _diContainer;
        private readonly SignalBus _signalBus;

        public UnitFactory(SignalBus signalBus, DiContainer diContainer)
        {
            _signalBus = signalBus;
            _diContainer = diContainer;
        }
        
        public UnitFacade Create(UnitSettings unitSettings, bool isPlayer)
        {
            var unit = _diContainer.InstantiatePrefabResourceForComponent<UnitFacade>(unitSettings.PrefabPath);
            unit.Initialize(_signalBus, unitSettings, isPlayer);
            return unit;
        }
    }
}