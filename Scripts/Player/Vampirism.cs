using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    public const string Fire2 = nameof(Fire2);

    private bool _isEmptying;
    private float _damage;
    private float _delayLightening;
    private WaitForSeconds _waitLightening;
    private Queue<Enemy> _queueOfEnemies;
    private Coroutine _coroutine;

    public event Action<Enemy> AmountChanged;
    public delegate bool ApplyTreatment(float treatment);
    public ApplyTreatment AddedHealth;

    private void Awake()
    {
        _damage = 1f;
        _delayLightening = 0.1f;
        _waitLightening = new WaitForSeconds(_delayLightening);
        _queueOfEnemies = new Queue<Enemy>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(Fire2) && _isEmptying == false)
        {
            Enemy enemy = _queueOfEnemies.Dequeue();

            if (enemy != null)
            {
                AmountChanged?.Invoke(enemy);
                TurnOnAbility(enemy);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _queueOfEnemies.Enqueue(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy _))
        {
            TurnOffAbility();
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

        if (enemy.enabled)
        {
            _queueOfEnemies.Enqueue(enemy);
        }
    }
}