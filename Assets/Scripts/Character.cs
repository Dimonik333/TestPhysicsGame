using UnityEngine;
public class Character : MonoBehaviour, IInteractionInvoker
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [Space]
    [SerializeField] private float _moveForce;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private AnimationCurve _accelerationHelpForce;

    private bool _isGrounded;
    private float _groundedAngle;

    private float _move;
    private bool _jump;

    public void Move(float direction)
    {
        _move = direction;
    }

    public void Jump()
    {
        _jump = true;
    }

    private void FixedUpdate()
    {
        var movePower = _move * _moveForce * Time.deltaTime;
        if (_groundedAngle <= 180)
        {
            if (_jump)
            {
                var jumpMultiplier = 1 - (_groundedAngle / 180);
                _rigidbody2D.AddForce(jumpMultiplier * _jumpForce * Vector2.up, ForceMode2D.Impulse);
                _jump = false;
            }

            if (movePower != 0)
            {
                var velocityX = _rigidbody2D.linearVelocityX;
                if (Mathf.Sign(velocityX) != Mathf.Sign(_move))
                {
                    var moveMultiplier = _accelerationHelpForce.Evaluate(Mathf.Abs(velocityX));
                    movePower *= moveMultiplier;
                }
            }
        }
        else
        {
            movePower /= 2;
        }

        var moveForce = Vector2.right * movePower;
        _rigidbody2D.AddForce(moveForce, ForceMode2D.Force);
        _jump = false;
        _isGrounded = false;
        _groundedAngle = float.MaxValue;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        _groundedAngle = float.MaxValue;
        foreach (var contact in collision.contacts)
        {
            var angle = Vector2.Angle(contact.normal, Vector2.up);
            _groundedAngle = Mathf.Min(_groundedAngle, angle);

            if (Vector2.Angle(contact.normal, Vector2.up) < 45)
            {
                _isGrounded = true;
                return;
            }
        }

        _isGrounded = false; // если ни одна точка не подошла
    }
}