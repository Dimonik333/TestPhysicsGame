using UnityEngine;
using UnityEngine.Pool;

public sealed class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject _explosionEffectPrefab;
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private float _minPower;
    [SerializeField] private float _force;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.relativeVelocity.magnitude >= _minPower)
            {
                _rigidbody.simulated = false;
                var rigidbodies = HashSetPool<Rigidbody2D>.Get();
                var colliders = Physics2D.OverlapCircleAll(transform.position, _radius);
                foreach (var collider in colliders)
                {
                    var rb = collider.attachedRigidbody;
                    if (rb != null)
                        rigidbodies.Add(rb);
                }
                foreach (var rb in rigidbodies)
                {
                    var direction = rb.position - (Vector2)transform.position;
                    rb.AddForce(direction.normalized * _force, ForceMode2D.Impulse);
                }
                rigidbodies.Clear();
                HashSetPool<Rigidbody2D>.Release(rigidbodies);

                Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            }
        }
    }
}