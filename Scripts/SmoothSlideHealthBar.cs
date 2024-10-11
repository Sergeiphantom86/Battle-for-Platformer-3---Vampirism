using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSlideHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;
    
    private float _smoothSlideDelta = 0.5f;

    private void Update()
    {
        _slider.transform.rotation = Quaternion.Euler(0,0,0);
    }

    protected  void OnEnable()
    {
        _health.AmountChanged += RefreshData;
    }

    protected void OnDisable()
    {
        _health.AmountChanged -= RefreshData;
    }

    protected  void RefreshData()
    {
        StartCoroutine(SmoothChangeSliderValue(_health.Amount));
    }

    private void Move(float target)
    {
        _slider.value = Mathf.MoveTowards(_slider.value, target, _smoothSlideDelta * Time.deltaTime);
    }

    private float GetTargetPosition(float target) => target /= _health.MaxAmount;

    private void RemoveFromScene()
    {
        if (_slider.value <= 0)
        {
            _slider.gameObject.SetActive(false);
        }
    }

    private IEnumerator SmoothChangeSliderValue(float target)
    {
        target = GetTargetPosition(target);

        while (_slider.value != target)
        {
            yield return null;

            Move(target);
        }

        RemoveFromScene();
    }
}