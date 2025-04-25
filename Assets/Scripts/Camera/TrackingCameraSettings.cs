using System;
using UnityEngine;

[Serializable]
public class TrackingCameraSettings
{
    [SerializeField] private float _maxDistance = 1f;

    [Header("Follow Time")]
    [Tooltip(" рива€ зависимости smoothTime от рассто€ни€ до цели")]
    [SerializeField] private AnimationCurve _smoothTimeByDistanceCurve;
    [SerializeField] private float _smoothTimeMultiplier = .5f;

    [Header("Size")]
    [Tooltip(" рива€ зависимости параметра Size от рассто€ни€ до цели")]
    [SerializeField] private AnimationCurve _sizeByDistanceCurve;
    [SerializeField] private float _sizeMultiplier;
    [SerializeField] private float _defaultSize = 4;

    public float MaxDistance => _maxDistance;

    public float SmoothTime(float distance)
        => _smoothTimeMultiplier * _smoothTimeByDistanceCurve.Evaluate(distance / _maxDistance);

    public float Size(float distance)
        => _defaultSize + _sizeMultiplier * _sizeByDistanceCurve.Evaluate(distance / _maxDistance);
}
