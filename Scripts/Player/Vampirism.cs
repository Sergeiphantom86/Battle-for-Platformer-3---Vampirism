using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(InputReader), typeof(NearestEnemyDetector), typeof(SlideAbilityBar))]

public class Vampirism : MonoBehaviour
{
    private Coroutine _coroutine;
    private WaitForSeconds _waitLightening;
    private InputReader _inputReader;
    private SlideAbilityBar _slideAbilityBar;
    private NearestEnemyDetector _nearestEnemyDetector;

    private bool _isPresed;
    private bool _isDeath;
    private float _damage;
    private float _delayLightening;
    private float _quantityCapacityOccupancy;

    public event Action<Enemy> AmountChanged;
    public event Func<float, bool> HealthHasChanged;

    private void Awake()
    {
        _damage = 0.5f;
        _delayLightening = 0.1f;
        _quantityCapacityOccupancy = 1;
        _waitLightening = new WaitForSeconds(_delayLightening);

        _inputReader = GetComponent<InputReader>();
        _slideAbilityBar = GetComponent<SlideAbilityBar>();
        _nearestEnemyDetector = GetComponent<NearestEnemyDetector>();
    }

    private void Update()
    {
        if (_quantityCapacityOccupancy <= 0)
        {
            _isPresed = false;
        }
    }

    private void OnEnable()
    {
        _inputReader.ButtonPressed += UseAbility;
        _nearestEnemyDetector.NewEnemyAppeared += DealDamageNearbyEnemy;
        _slideAbilityBar.EffectAbilityWorked += GetConfirmationOfEmptiness;
        _nearestEnemyDetector.CameOut += TurnOffAbility;
    }

    private void OnDisable()
    {
        _inputReader.ButtonPressed -= UseAbility;
        _nearestEnemyDetector.NewEnemyAppeared -= DealDamageNearbyEnemy;
        _slideAbilityBar.EffectAbilityWorked -= GetConfirmationOfEmptiness;
        _nearestEnemyDetector.CameOut -= TurnOffAbility;
    }

    private void UseAbility(bool isPressed)
    {
        _isPresed = isPressed;
    }

    private void DealDamageNearbyEnemy(Enemy enemy)
    {
        if (_isPresed)
        {
            if (_quantityCapacityOccupancy > 0)
            {
                AmountChanged?.Invoke(enemy);
                TurnOnAbility(enemy);
            }
        }
    }

    private void GetConfirmationOfEmptiness(float quantityCapacityOccupancy)
    {
        _quantityCapacityOccupancy = quantityCapacityOccupancy;
    }

    private void TurnOnAbility(Enemy enemy)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(TakeHealth(enemy));
        }
    }

    private void TurnOffAbility(Enemy enemy)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);

            _coroutine = null;
        }
    }

    private void DetermineDeathEnemy(bool isDeath)
    {
        _isDeath = isDeath;
    }

    private IEnumerator TakeHealth(Enemy enemy)
    {
        while (_quantityCapacityOccupancy > 0 && _isDeath == false)
        {
            enemy.TakeDamage(_damage);

            HealthHasChanged?.Invoke(_damage);

            enemy.HeDied += DetermineDeathEnemy;

            yield return _waitLightening;
        }

        enemy.HeDied -= DetermineDeathEnemy;

        _isDeath = false;
    }
}