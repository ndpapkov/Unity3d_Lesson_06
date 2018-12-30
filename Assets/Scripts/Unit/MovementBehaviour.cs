using System;
using Storage;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Unit
{
    public class MovementBehaviour : MonoBehaviour
    {
        public BehaviorSubject<bool> IsMoving { get; } = new BehaviorSubject<bool>(false);

        [SerializeField] 
        private NavMeshAgent _agent;

        private IDisposable _followingTarget;

        public void Initialize(MovementSettings movementSettings)
        {
            _agent.speed = movementSettings.MovementSpeed;
            _agent.stoppingDistance = movementSettings.StoppingDistance;
        }

        private void Awake()
        {
            this.ObserveEveryValueChanged(_ => _.IsAgentMoving())
                .Subscribe(value => IsMoving.OnNext(value))
                .AddTo(this);
        }

        private void OnDestroy()
        {
            StopFollowing();
        }
        
        private bool IsAgentMoving()
        {
            return _agent.velocity.sqrMagnitude > Mathf.Epsilon ||
                   _agent.remainingDistance > _agent.stoppingDistance;
        }

        public void MoveToPosition(Vector3 position)
        {
            StopFollowing();
            SetNewDestination(position);
        }
    
        public void FollowTarget(Transform target)
        {
            StopFollowing();
        
            _followingTarget = target
                .ObserveEveryValueChanged(someTarget => someTarget.position)
                .Subscribe(SetNewDestination);
        }

        public void StopFollowing()
        {
            _followingTarget.SafeDispose();
        }

        private void SetNewDestination(Vector3 position)
        {
            _agent.destination = position;
        }
    }
}