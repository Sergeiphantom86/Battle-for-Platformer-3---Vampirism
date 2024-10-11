using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Mover : MonoBehaviour
{
    private float _speed;
    protected Rigidbody2D Rigidbody;

    private void Start()
    {
        _speed = 10;
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Walk(float speed)
    {
        Rigidbody.velocity = Vector2.MoveTowards(Rigidbody.velocity, GetTarget(speed), _speed);
    }

    private Vector2 GetTarget(float move)
    {
        return new(move * _speed, Rigidbody.velocity.y);
    }
}