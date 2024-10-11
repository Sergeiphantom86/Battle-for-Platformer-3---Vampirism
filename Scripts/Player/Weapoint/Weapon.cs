using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public const string Fire1 = nameof(Fire1);

    [SerializeField] private FirePoint _firePoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Animator _animator;
    [Range(0.1f, 1f)][SerializeField] private float _shotDelay;

    private WaitForSeconds _wait;

    public bool IsFire { get; private set; }

    private void Start()
    {
        _wait = new WaitForSeconds(_shotDelay);
    }

    private void Update()
    {
        if (Input.GetButtonDown(Fire1))
        {
            StartCoroutine(DelayShot());
        }
    }

    private void Shoot()
    {
        Instantiate(_bulletPrefab,
       _firePoint.transform.position,
       _firePoint.transform.rotation);

        IsFire = false;
    }

    private IEnumerator DelayShot()
    {
        if (IsFire == false)
        {
            IsFire = true;

            yield return _wait;

            Shoot();
        }
    }
}