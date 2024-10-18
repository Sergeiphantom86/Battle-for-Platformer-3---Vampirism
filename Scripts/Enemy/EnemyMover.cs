using System;
using UnityEngine;

[RequireComponent(typeof(Flipper))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private int _minPositionX;
    [SerializeField] private int _maxPositionX;
    [SerializeField] private EnemyAttackZone _enemyAttackZone;

    private Flipper _flipper;
    private float _targetPosition;
    private bool _isDeath;

    public float HorizontalMove { get; private set; }

    private void Awake()
    {
        HorizontalMove = 1f;
        _flipper = GetComponent<Flipper>();
        _targetPosition = transform.position.x;
    }

    private void Update()
    {
        Walk(GetNewVector());
        SetSpeed();
        DetermineDirection();
    }

    public void SetConfirmationDeath(bool isDeath)
    {
        _isDeath = isDeath;
    }

    private void Walk(Vector2 vector2)
    {
        transform.position = Vector2.MoveTowards(transform.position, vector2, HorizontalMove * Time.fixedDeltaTime);
    }

    private void DetermineDirection()
    {
        if (Mathf.Abs(_targetPosition) - Mathf.Abs(transform.position.x) == 0)
        {
            SetTargetPosition();

            if (transform.position.x >= _targetPosition)
            {
                _flipper.FlipCharacter(-HorizontalMove);
            }
            else
            {
                _flipper.FlipCharacter(HorizontalMove);
            }
        }
    }

    private void SetSpeed()
    {
        if (_enemyAttackZone.IsCame || _isDeath)
        {
            HorizontalMove = 0;
        }
        else
        {
            HorizontalMove = 1;
        }
    }

    private void SetDirection(float horizontalMove)
    {
        _flipper.FlipCharacter(horizontalMove);
    }

    private Vector2 GetNewVector()
    {
        return new Vector2(_targetPosition, transform.position.y);
    }

    private void SetTargetPosition()
    {
        if (_enemyAttackZone.IsLocatedInTargetZone)
        {
            _targetPosition = _enemyAttackZone.TargetPosition;
        }
        else
        {
            _targetPosition = GetRandomTarget();
        }
    }

    private int GetRandomTarget()
    {
        return UnityEngine.Random.Range(_minPositionX, _maxPositionX);
    }
}