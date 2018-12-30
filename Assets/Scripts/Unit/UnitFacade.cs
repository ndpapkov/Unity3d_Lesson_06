using Storage;
using UniRx;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class UnitFacade : MonoBehaviour
    {
        public bool IsPlayer { get; private set; }
        
        [SerializeField]
        private MovementBehaviour _movementBehaviour;
        [SerializeField]
        private HealthHandler _health;
        [SerializeField]
        private AttackBehaviour _attackBehaviour;
        [SerializeField]
        private UnitAnimatorController _animatorController;

        private SignalBus _signalBus;

        public void Initialize(SignalBus bus, UnitSettings settings, bool isPlayer)
        {
            _signalBus = bus;
            IsPlayer = isPlayer;

            if (IsPlayer)
            {
                _signalBus.Subscribe<UnitMoveSignal>(MoveAndTryAttack);
            }
            
            _movementBehaviour.Initialize(settings.MovementSettings);
            _health.Initialize(settings.BaseHealth);
            _attackBehaviour.Initialize(settings.AttackSettings);
        }

        private void OnDestroy()
        {
            if (IsPlayer)
            {
                _signalBus.Unsubscribe<UnitMoveSignal>(MoveAndTryAttack);
            }
        }
        
        private void Start()
        {
            _health.IsAlive.Subscribe(OnAliveStateChanged).AddTo(this);
            
            _animatorController.Initialize(
                _health.IsAlive, 
                _movementBehaviour.IsMoving, 
                _attackBehaviour.IsAttacking);
        }
        
        private void OnAliveStateChanged(bool isAlive)
        {
            if (!isAlive)
            {
                _attackBehaviour.ResetTarget();
                _movementBehaviour.StopFollowing();
            }
        }

        public void MoveAndTryAttack(UnitMoveSignal moveSignal)
        {
            if (moveSignal.FollowingTarget != null)
            {
                _movementBehaviour.FollowTarget(moveSignal.FollowingTarget);
                _attackBehaviour.SetTarget(moveSignal.FollowingTarget.GetComponent<HealthHandler>());
            }
            else
            {
                _movementBehaviour.MoveToPosition(moveSignal.MovePosition);
            }
        }
    }
}