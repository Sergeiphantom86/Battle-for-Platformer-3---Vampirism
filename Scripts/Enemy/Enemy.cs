using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(Health), typeof(Collider2D))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private CharacterAnimations _characterAnimations;
    [SerializeField] private PaintAttack _paintAttack;

    private Health _health;
    private EnemyMover _enemyMover;
    private Collider2D _collider;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _health = GetComponent<Health>();
        _collider = GetComponent<Collider2D>();
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
            _paintAttack.ChangeColor();
        }
    }

    public void TakeDamage(float damage)
    {
        _health.ApplyDamage(damage);
        
        if (_health.Amount <= 0)
        {
            WithdrawFromBattle();
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