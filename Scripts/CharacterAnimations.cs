using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private const string Speed = nameof(Speed);
    private const string Died = nameof(Died);
    private const string IsAttack = nameof(IsAttack);

    [SerializeField] private Animator _animator;

    public void Run(float direction)
    {
        _animator.SetFloat(Speed, Mathf.Abs(direction));
    }

    public void Die(bool died)
    {
        _animator.SetBool(Died, died);
    }

    public void Attack(bool isAttack)
    {
        _animator.SetBool(IsAttack, isAttack);
    }
}