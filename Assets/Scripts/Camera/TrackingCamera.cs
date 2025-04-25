using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    private Vector2 _targetPosition; // позици€ наблюдаемого объекта
    private Vector2 _followPosition; // догон€юща€ позици€
    private Vector2 _outrunPosition; // обгон€юща€ позици€
    private Vector2 _currentVelocity = Vector2.zero;

    [SerializeField] private Transform _targetTransform;
    [Space]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _trackerTransform;
    [SerializeField] private TrackingCameraSettings _settings;


    void Start()
    {
        if (_targetTransform == null)
            return;
        Init();
    }

    private void OnEnable()
    {
        if (_targetTransform == null)
            enabled = false;
    }

    public void SetTarget(Transform target)
    {
        if (target == null)
            return;
        _targetTransform = target;
        Init();
        enabled = true;
    }

    private void Init()
    {
        _trackerTransform.position = _targetTransform.position;
        _followPosition = _trackerTransform.position;
        _outrunPosition = _trackerTransform.position;
    }

    private void LateUpdate()
    {
        CalcPositions();
    }

    private void CalcPositions()
    {
        _targetPosition = _targetTransform.position;
        var followDirection = _targetPosition - _followPosition;
        var followDistance = followDirection.magnitude;

        var smoothTime = _settings.SmoothTime(followDistance);
        _followPosition = Vector2.SmoothDamp(_followPosition, _targetPosition, ref _currentVelocity, smoothTime);
        followDirection = _targetPosition - _followPosition;
        followDistance = followDirection.magnitude;
        if (followDistance > _settings.MaxDistance)
        {
            var distanceMultiplier = followDistance / _settings.MaxDistance;
            followDirection /= distanceMultiplier;
            _followPosition = _targetPosition - followDirection;
        }
        _outrunPosition = _targetPosition + followDirection;
        _trackerTransform.position = _outrunPosition;
        _camera.orthographicSize = _settings.Size(followDistance);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        var color = Gizmos.color;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_followPosition, .25f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_outrunPosition, .25f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_targetPosition, .25f);

        Gizmos.color = color;
    }
}
