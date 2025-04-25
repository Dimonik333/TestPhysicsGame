using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class TouchInputController : MonoBehaviour
{
    [SerializeField] private TouchButton _leftButton;
    [SerializeField] private TouchButton _rightButton;
    [SerializeField] private TouchButton _jumpButton;

    public float Horizontal { get; private set; }
    public bool JumpPressed { get; private set; }

    private void OnEnable()
    {
        _jumpButton.OnPressed += OnJumpButtonPressed;
    }


    private void OnDisable()
    {
        _jumpButton.OnPressed -= OnJumpButtonPressed;
    }

    private void OnJumpButtonPressed()
    {
        JumpPressed = true;
    }


    private void Update()
    {
        Horizontal = 0;
        if (_leftButton.IsHeld)
            Horizontal = -1;
        if (_rightButton.IsHeld)
            Horizontal = 1f;
    }

    private void LateUpdate()
    {
        JumpPressed = false;
    }
}
