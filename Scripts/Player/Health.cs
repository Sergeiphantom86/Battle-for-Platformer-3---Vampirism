using System;
using UnityEngine;

[RequireComponent(typeof(PaintAttack))]

public class Health : MonoBehaviour
{
    [SerializeField][Range(50, 100)] private float _maxAmount;
    [SerializeField][Range(50, 100)] private float _amount;

    public event Action AmountChanged;

    public float Amount
    {
        get => _amount;

        private set
        {
            if (value == _amount)
                return;

            _amount = Mathf.Clamp(value, 0, _maxAmount);

            AmountChanged?.Invoke();
        }
    }

    public float MaxAmount => _maxAmount;

    public void ApplyDamage(float damage)
    {
        if (_amount > 0)
        {
            Amount -= Mathf.Abs(damage);
        }
    }

    public bool ApplyTreatment(float health)
    {
        if (_amount < _maxAmount)
        {
            Amount += health;

            return true;
        }
        else
        {
            return false;
        }
    }
}