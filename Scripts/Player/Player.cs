using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Weapon), typeof(Health))]

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterAnimations _animation;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PaintAttack _paintAttack;
    [SerializeField] private Vampirism _vampirism;

    private InputReader _inputReader;
    private Health _health;
    private Weapon _weapon;

    private void Start()
    {
        _inputReader = GetComponent<InputReader>();
        _health = GetComponent<Health>();
        _weapon = GetComponent<Weapon>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            _playerMover.Move(_inputReader.Direction);
        }

        _animation.Run(_inputReader.Direction);
        _animation.Attack(_weapon.IsFire);

        if (_inputReader.GetIsJump())
        {
            _playerMover.Jump();
        }
    }

    private void OnEnable()
    {
        _vampirism.HealthHasChanged += ApplyTreatment;
    }

    private void OnDisable()
    {
        _vampirism.HealthHasChanged -= ApplyTreatment;
    }

    public void TakeDamage(float damage)
    {
        _health.ApplyDamage(damage);
        _paintAttack.ChangeColor();

        if (_health.Amount <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public bool ApplyTreatment(float health)
    {
        return _health.ApplyTreatment(health);
    }
}