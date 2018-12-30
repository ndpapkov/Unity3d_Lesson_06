using UniRx;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(Animator))]
    public class UnitAnimatorController : MonoBehaviour
    {
        private const string ANIMATOR_IDLE_TRIGGER = "Idle";
        private const string ANIMATOR_MOVE_TRIGGER = "Run";
        private const string ANIMATOR_ATTACK_TRIGGER = "MeleeAttack";
        private const string ANIMATOR_DEAD_TRIGGER = "Death";
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Initialize(
            BehaviorSubject<bool> isAlive, 
            BehaviorSubject<bool> isMoving,
            BehaviorSubject<bool> isAttacking)
        {
            isAlive.Subscribe(value => SetTrigger(ANIMATOR_DEAD_TRIGGER, !value)).AddTo(this);
            isMoving.Subscribe(UpdateMovingState).AddTo(this);
            isAttacking.Subscribe(value => SetTrigger(ANIMATOR_ATTACK_TRIGGER, value)).AddTo(this);
        }

        private void UpdateMovingState(bool isMoving)
        {
            if (isMoving)
            {
                _animator.ResetTrigger(ANIMATOR_IDLE_TRIGGER);
                _animator.SetTrigger(ANIMATOR_MOVE_TRIGGER);
            }
            else
            {
                _animator.ResetTrigger(ANIMATOR_MOVE_TRIGGER);
                _animator.SetTrigger(ANIMATOR_IDLE_TRIGGER);
            }
        }
        
        private void SetTrigger(string key, bool value)
        {
            if (value)
            {
                _animator.SetTrigger(key);
            }
        }

    }
}