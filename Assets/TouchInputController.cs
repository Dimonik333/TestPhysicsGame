using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class TouchInputController : MonoBehaviour
{
    [SerializeField] private TouchButton _leftButton;
    [SerializeField] private TouchButton _rightButton;
    [SerializeField] private Button _jumpButton;

    public float Horizontal { get; private set; }
    public bool JumpPressed { get; private set; }

    private void OnEnable()
    {
        _jumpButton.onClick.AddListener(OnJumpButtonClick);
    }

    private void OnDisable()
    {
        _jumpButton.onClick.RemoveListener(OnJumpButtonClick);
    }

    private void OnJumpButtonClick()
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
