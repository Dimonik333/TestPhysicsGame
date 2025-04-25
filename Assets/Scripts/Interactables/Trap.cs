using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private RigidbodyTriggerArea _triggerArea;
    [SerializeField] private Rigidbody2D[] _rigidbodies;

    private void OnEnable()
    {
        _triggerArea.BodyEnter += OnBodyEnter;
    }

    private void OnBodyEnter(Rigidbody2D rigidbody)
    {
        if (!rigidbody.TryGetComponent<IInteractionInvoker>(out var invoker))
            return;

        foreach (var rb in _rigidbodies)
            rb.simulated = true;
        enabled = false;
    }
}
