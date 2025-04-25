using System;
using UnityEngine;

public sealed class Finish : MonoBehaviour
{
    [SerializeField] private RigidbodyTriggerArea _triggerArea;
    public event Action PlayerFinished;

    private void OnEnable()
    {
        _triggerArea.BodyEnter += OnBodyEnter;
    }

    private void OnBodyEnter(Rigidbody2D rigidbody)
    {
        if (!rigidbody.TryGetComponent<Character>(out var character))
            return;
        PlayerFinished?.Invoke();
        enabled = false;
    }
}
