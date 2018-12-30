using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    public class AnimatorRandomParameterGenerator : MonoBehaviour
    {
        [SerializeField] 
        private Animator _animator;

        private void Awake()
        {
            Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => _animator.SetFloat("RandomFloat", Random.Range(0, 3)))
                .AddTo(this);
        }
    }
}