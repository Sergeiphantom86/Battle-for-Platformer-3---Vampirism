using System;
using System.Collections.Generic;
using UnityEngine;

public class Closest : MonoBehaviour
{
    private List<Enemy> _listOfEnemies;
    private Enemy _enemy;

    public Action IsOut;

    private void Awake()
    {
        _listOfEnemies = new List<Enemy>();
    }

    private void Update()
    {
        FindClosestEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _listOfEnemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (_enemy == enemy)
            {
                IsOut?.Invoke();
                _listOfEnemies.Remove(enemy);
            }
        }
    }

    public Enemy GetEnemy()
    {
        return _enemy;
    }

    private void FindClosestEnemy()
    {
        float distance = Mathf.Infinity;

        foreach (Enemy enimy in _listOfEnemies)
        {
            if (enimy != null)
            {
                Vector2 diff = enimy.transform.position - transform.position;

                float curDistance = diff.sqrMagnitude;

                if (curDistance < distance)
                {
                    _enemy = enimy;
                    distance = curDistance;
                }
            }
        }
    }
}