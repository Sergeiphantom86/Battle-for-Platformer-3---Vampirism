using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Closest))]

public class Vampirism : MonoBehaviour
{
    private Coroutine _coroutine;
    private InputReader _inputReader;
    private Closest _closestEnemy;
    private WaitForSeconds _waitLightening;

    private float _delayLightening;
    private float _damage;
    private bool _isEmptying;

    public Action<Enemy> AmountChanged;
    public Func<float, bool> AddedHealth;

    private void Awake()
    {
        _damage = 1f;
        _delayLightening = 0.1f;
        _waitLightening = new WaitForSeconds(_delayLightening);
        _inputReader = GetComponent<InputReader>();
        _closestEnemy = GetComponent<Closest>();
    }

    private void OnEnable()
    {
        _inputReader.ButtonPressed += UseAbility;
        _closestEnemy.IsOut += TurnOffAbility;
    }

    private void OnDisable()
    {
        _inputReader.ButtonPressed -= UseAbility;
        _closestEnemy.IsOut += TurnOffAbility;
    }

    private void UseAbility()
    {
        if (_isEmptying == false)
        {
            if (_closestEnemy.GetEnemy() != null)
            {
                AmountChanged?.Invoke(_closestEnemy.GetEnemy());
                TurnOnAbility(_closestEnemy.GetEnemy());
            }
        }
    }

    public void GetConfirmationOfEmptiness(bool isEmptying)
    {
        _isEmptying = isEmptying;
    }

    private void TurnOnAbility(Enemy enemy)
    {
        _coroutine = StartCoroutine(TakeHealth(enemy));
    }

    private void TurnOffAbility()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator TakeHealth(Enemy enemy)
    {
        while (_isEmptying == false)
        {
            enemy.TakeDamage(_damage);
            AddedHealth?.Invoke(_damage);

            yield return _waitLightening;
        }
    }
}