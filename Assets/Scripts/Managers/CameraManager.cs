using UniRx;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(Camera))]
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] 
        private float _moveThreshold;
    
        [SerializeField, Tooltip("Unit Per Sec")] 
        private float _cameraMovingSpeed;

        [SerializeField]
        private Transform _cameraPivot;

        [SerializeField] 
        private Transform _followingObject;

        private Vector3 _targetPosition;
        private bool _isMoving;

        private void Awake()
        {
            _followingObject
                .ObserveEveryValueChanged(target => target.position)
                .Where(ShouldMoveToTarget)
                .Subscribe(UpdateCameraMovingTarget)
                .AddTo(this);
        }

        private bool ShouldMoveToTarget(Vector3 target)
        {
            return Vector3.Distance(target, _cameraPivot.position) > _moveThreshold;
        }

        private void UpdateCameraMovingTarget(Vector3 position)
        {
            _isMoving = true;
            _targetPosition = position;
        }    
    
        private void Update()
        {
            if (_isMoving)
            {
                MoveCamera(_targetPosition);
            
                _isMoving = ShouldMoveToTarget(_targetPosition);
            }
        }

        private void MoveCamera(Vector3 target)
        {
            var distanceToTarget = Vector3.Distance(target, _cameraPivot.position);
            var moveFractionPerFrame = _cameraMovingSpeed * Time.deltaTime / distanceToTarget;
            var newPivotPosition = Vector3.Lerp(_cameraPivot.position, _targetPosition, moveFractionPerFrame);
            var currentOffset = newPivotPosition - _cameraPivot.position;

            transform.position += currentOffset;
        }
    }
}

