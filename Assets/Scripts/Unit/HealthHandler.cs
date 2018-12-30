using UniRx;
using UnityEngine;

namespace Unit
{
    public class HealthHandler : MonoBehaviour
    {
        public BehaviorSubject<bool> IsAlive { get; } = new BehaviorSubject<bool>(false);
        public BehaviorSubject<float> Health { get; } = new BehaviorSubject<float>(0);

        public void Initialize(float baseHealth)
        {
            Health.OnNext(baseHealth);
        }

        private void Awake()
        {
            Health.Subscribe(OnHealthChanged).AddTo(this);
        }
    
        private void OnHealthChanged(float newHealth)
        {
            IsAlive.OnNext(newHealth > 0);
        }
    
        public void TakeDamage(float damage)
        {
            Health.OnNext(Mathf.Max(0, Health.Value - damage));
        }
    }
}
