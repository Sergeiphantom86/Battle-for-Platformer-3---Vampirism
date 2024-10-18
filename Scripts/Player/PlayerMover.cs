using UnityEngine;

[RequireComponent(typeof(Flipper), typeof(GroundDetector))]

public class PlayerMover : Mover
{
    [SerializeField] private float _jumpForce = 400f;

    private Flipper _flipper;
    private GroundDetector _groundDetector;

    private bool _grounded;

    private void Awake()
    {
        _flipper = GetComponent<Flipper>();
        _groundDetector = GetComponent<GroundDetector>();
    }

    private void Update()
    {
        _grounded = _groundDetector.IsGround();
    }

    public void Move(float move)
    {
        Walk(move);

        _flipper.FlipCharacter(move);
    }

    public void Jump()
    {
        if (_grounded)
        {
            _grounded = false;
            Rigidbody.velocity = Vector2.zero;
            Rigidbody.AddForce(new Vector2(0, _jumpForce));
        }
    }
}