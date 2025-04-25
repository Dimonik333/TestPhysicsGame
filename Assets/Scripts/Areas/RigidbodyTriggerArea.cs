using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RigidbodyTriggerArea : MonoBehaviour
{
    private Dictionary<Rigidbody2D, HashSet<Collider2D>> _targets;

    public event Action<Rigidbody2D> BodyEnter;
    public event Action<Rigidbody2D> BodyExit;


    private void OnEnable()
    {
        _targets = DictionaryPool<Rigidbody2D, HashSet<Collider2D>>.Get();
    }

    private void OnDisable()
    {
        foreach (var kvp in _targets)
            HashSetPool<Collider2D>.Release(kvp.Value);
        DictionaryPool<Rigidbody2D, HashSet<Collider2D>>.Release(_targets);
        _targets = null;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        var rigidbody = collider.attachedRigidbody;
        if (rigidbody == null)
            return;
        if (!_targets.TryGetValue(rigidbody, out var colliders))
        {
            colliders = HashSetPool<Collider2D>.Get();
            _targets.Add(rigidbody, colliders);
            BodyEnter?.Invoke(rigidbody);
        }
        colliders.Add(collider);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        var rigidbody = collision.attachedRigidbody;
        if (rigidbody == null)
            return;
        if (_targets.TryGetValue(rigidbody, out var colliders))
        {
            colliders.Remove(collision);
            if (colliders.Count != 0)
                return;
            HashSetPool<Collider2D>.Release(colliders);
            _targets.Remove(rigidbody);
            BodyExit?.Invoke(rigidbody);
        }
    }

    public void Foreach(Action<Rigidbody2D> action)
    {
        if (_targets == null)
            return;
        foreach (var target in _targets)
            action(target.Key);
    }
}