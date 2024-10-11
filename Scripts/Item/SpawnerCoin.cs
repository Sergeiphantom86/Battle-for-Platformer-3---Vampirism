using System.Collections;
using UnityEngine;

public class SpawnerCoin : MonoBehaviour
{
    [SerializeField] private Coin _coinsPrefab;
    [SerializeField] private int _respawnDuration;

    private Coin _coin;
    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new WaitForSeconds(_respawnDuration);
    }

    private void OnEnable()
    {
        _coin = Instantiate(_coinsPrefab, _coinsPrefab.GetRandomPosition(), Quaternion.identity);

        _coin.Collected += Respawn;
    }

    private void OnDisable()
    {
        _coin.Collected -= Respawn;
    }

    private void Respawn()
    {
        StartCoroutine(SpawnCoinWithDelay());
    }

    private IEnumerator SpawnCoinWithDelay()
    {
        yield return _wait;

        _coin.gameObject.SetActive(true);
        _coin.transform.position = _coin.GetRandomPosition();
    }
}
