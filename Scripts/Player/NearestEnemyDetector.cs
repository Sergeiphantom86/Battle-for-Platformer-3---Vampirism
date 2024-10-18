using System;
using System.Collections.Generic;
using UnityEngine;

public class NearestEnemyDetector : MonoBehaviour
{
    private List<Enemy> _enemies;
    private Enemy _newEnemy;
    private string _name;

    public event Action<Enemy> CameOut;
    public event Action<Enemy> NewEnemyAppeared;

    private void Awake()
    {
        _enemies = new List<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _enemies.Add(enemy);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _newEnemy = FindClosestEnemy();

            NewEnemyAppeared?.Invoke(_newEnemy);

            if (_newEnemy.name != _name)
            {
                if (_name != null)
                {
                    CameOut?.Invoke(_newEnemy);
                }

                _name = _newEnemy.name;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (_newEnemy != null)
            {
                _enemies.Remove(enemy);

                if (_newEnemy.name == enemy.name)
                {
                    CameOut?.Invoke(enemy);

                    if (_enemies.Count <= 0)
                    {
                        _name = null;
                    }
                }
            }
        }
    }

    private Enemy FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Enemy nearEnemy = null;

        foreach (Enemy enemy in _enemies)
        {
            Vector2 diff = enemy.transform.position - transform.position;

            float currentDistance = diff.sqrMagnitude;

            if (currentDistance < distance)
            {
                nearEnemy = enemy;
                distance = currentDistance;
            }
        }

        return nearEnemy;
    }
}