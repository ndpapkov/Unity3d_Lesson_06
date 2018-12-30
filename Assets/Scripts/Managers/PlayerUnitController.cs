using System.Linq;
using UniRx;
using UniRx.Triggers;
using Unit;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class PlayerUnitController : MonoBehaviour
    {
        private readonly RaycastHit[] _hits = new RaycastHit[10];
        private readonly UnitMoveSignal _moveSignal = new UnitMoveSignal();
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonUp(1))
                .Select(_ => Input.mousePosition)
                .Subscribe(OnClick)
                .AddTo(this);
        }

        private void OnClick(Vector3 position)
        {
            var cameraRay = Camera.main.ScreenPointToRay(position);
            var hitsCount = Physics.RaycastNonAlloc(cameraRay, _hits);
            if (hitsCount > 0)
            {
                _moveSignal.MovePosition = _hits[0].point;
                var unitsUnderMouse = _hits.Take(hitsCount).Select(hit => hit.collider.GetComponent<UnitFacade>());
                var firstEnemy = unitsUnderMouse.FirstOrDefault(unit => unit != null && !unit.IsPlayer);
                _moveSignal.FollowingTarget = firstEnemy?.transform;

                _signalBus.Fire(_moveSignal);
            }
        }
    }
}