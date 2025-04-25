using UnityEngine;

public sealed class RigidbodyLinearForceArea : RigidbodyForceArea
{
    [SerializeField] private RigidbodyTriggerArea _rigidbodyTriggerArea;
    [SerializeField] private Vector3 _forceDirection = Vector3.up;
    [SerializeField] private float _forceMultiplier = 500;

    
    private void FixedUpdate()
        => _rigidbodyTriggerArea.Foreach(GetForce);


    private void GetForce(Rigidbody2D rigidbody2D)
    {
        var force = Power * _forceMultiplier * Time.fixedDeltaTime * _forceDirection;
        rigidbody2D.AddForce(force);
    }
}
