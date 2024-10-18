using System;
using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(Health))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private CharacterAnimations _characterAnimations;
    [SerializeField] private PaintAttack _paintAttack;
    [SerializeField] private Collider2D _collider;

    private Health _health;
    private EnemyMover _enemyMover;

    public event Action <bool> HeDied;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        _characterAnimations.Run(_enemyMover.HorizontalMove);
        gameObject.SetActive(_paintAttack.IsOpaque);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            Destroy(bullet.gameObject);
            TakeDamage(bullet.Damage);
        }
    }

    public void TakeDamage(float damage)
    {
        _health.ApplyDamage(damage);
        _paintAttack.ChangeColor();

        if (_health.Amount <= 0)
        {
            WithdrawFromBattle();
            HeDied?.Invoke(true);
        }
    }

    private void WithdrawFromBattle(bool died = true)
    {
        _collider.enabled = false;
        _characterAnimations.Die(died);
        _enemyMover.SetConfirmationDeath(died);
        _paintAttack.TryDelete();
    }
}