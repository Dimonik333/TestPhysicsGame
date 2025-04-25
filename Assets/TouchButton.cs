using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnPressed;
    public event Action OnReleased;

    private bool _isHeld;
    public bool IsHeld => _isHeld;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHeld = true;
        OnPressed?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHeld = false;
        OnReleased?.Invoke();
    }
}
