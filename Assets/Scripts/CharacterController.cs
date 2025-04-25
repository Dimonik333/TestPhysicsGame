using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private TouchInputController _input;

    private void Update()
    {
        var horizontal = _input.Horizontal;
        var jump = _input.JumpPressed;


#if UNITY_EDITOR
        //horizontal = Input.GetAxis("Horizontal");
        //jump = Input.GetKeyDown(KeyCode.W);
#endif

        _character.Move(horizontal);
        if (jump)
            _character.Jump();
    }
}
