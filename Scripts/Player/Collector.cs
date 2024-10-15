using System;
using UnityEngine;

[RequireComponent(typeof(Player))]

public class Collector : MonoBehaviour
{
    private Bag _bag;
    private Player _player;

    private void Awake()
    {
        _bag = new Bag();
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Item item))
        {
            if (item is Apple apple)
            {
                TryPickApple(apple);
            }
            else if(item is Coin coin)
            {
                coin.Collect();
                _bag.AddCoin(coin.Value);
            }
        }
    }

    private void TryPickApple(Apple apple)
    {
        if (_player == null)
            throw new ArgumentOutOfRangeException(nameof(_player));

        bool fullHealth = _player.ApplyTreatment(apple.Value);

        if (fullHealth)
        {
            apple.Collect();
        }
    }
}