using UnityEngine;

public class SpawnerApple : MonoBehaviour
{
    [SerializeField] private Apple _applePrefab;

    private void Start()
    {
        CreateApple(_applePrefab);
    }

    private void CreateApple(Apple apple, int quantityApple = 5)
    {
        for (int i = 0; i <= quantityApple; i++)
        {
            Instantiate(apple, _applePrefab.GetRandomPosition(), Quaternion.identity);
        }
    }
}