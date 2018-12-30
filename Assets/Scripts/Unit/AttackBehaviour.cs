using Storage;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Unit
{
    public class AttackBehaviour : MonoBehaviour
    {
        public BehaviorSubject<bool> IsAttacking { get; } = new BehaviorSubject<bool>(false);

        private float _minimalDistance;
        private float _cooldown;
        private float _power;

        private float _lastAttackTime = float.MinValue;
        private HealthHandler _enemy;

        public void Initialize(AttackSettings attackSettings)
        {
            _power = attackSettings.AttackPower;
            _cooldown = attackSettings.AttackCooldown;
            _minimalDistance = attackSettings.AttackDistance;
        }

        private void Awake()
        {
            this.UpdateAsObservable()
                .Where(_ => CanAttack(_enemy))
                .Subscribe(_ => Attack(_enemy))
                .AddTo(this);
        }
        
        public void SetTarget(HealthHandler enemy)
        {
            _enemy = enemy;
        }

        public void ResetTarget()
        {
            _enemy = null;
        }

        private void Attack(HealthHandler enemy)
        {
            _lastAttackTime = Time.time;
            
            IsAttacking.OnNext(true);
            enemy.TakeDamage(_power);
            IsAttacking.OnNext(false);
        }
        
        private bool CanAttack(HealthHandler enemy)
        {
            if (enemy == null || !enemy.IsAlive.Value)
            {
                return false;
            }
            
            var distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            var isAttackDistanceReached = distanceToEnemy < _minimalDistance;
            var isReloaded = _lastAttackTime + _cooldown < Time.time;
            
            return isAttackDistanceReached && isReloaded;
        }
    }
}