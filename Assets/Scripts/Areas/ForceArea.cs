using UnityEngine;

public abstract class RigidbodyForceArea : MonoBehaviour
{
    [SerializeField]
    private float _force;
    public float Power { get => _force; set => _force = value; }
}