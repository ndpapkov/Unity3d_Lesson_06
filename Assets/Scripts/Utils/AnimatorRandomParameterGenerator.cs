using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    public class AnimatorRandomParameterGenerator : MonoBehaviour
    {
        private const float GenerationIntervalInSec = 1f;
        
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private float _minValue;
        [SerializeField]
        private float _maxValue = 3;
        
        private void Awake()
        {
            Observable
                .Interval(TimeSpan.FromSeconds(GenerationIntervalInSec))
                .Subscribe(_ => SetRandomNumber())
                .AddTo(this);
        }

        private void SetRandomNumber()
        {
            _animator.SetFloat("RandomFloat", Random.Range(_minValue, _maxValue));
        }
    }
}