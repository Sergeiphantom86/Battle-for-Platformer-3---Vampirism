using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlideAbilityBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Vampirism _vampirism;

    private float _delay;
    private float _smoothSlideDelta;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
        _smoothSlideDelta = 1;
        _delay = 0.02f;
    }

    private void OnEnable()
    {
        _vampirism.AmountChanged += ChangeValue;
    }

    private void OnDisable()
    {
        _vampirism.AmountChanged -= ChangeValue;
    }

    private void Move(float target)
    {
        _slider.value = Mathf.MoveTowards(_slider.value, target, _smoothSlideDelta * Time.deltaTime);
    }

    private void ChangeValue(Enemy enemy)
    {
        if (_slider.value == _slider.maxValue)
        {
            StartCoroutine(WaitForReduction(_slider.minValue));
        }
    }

    private IEnumerator WaitForReduction( float target)
    {
        while (_slider.value > target)
        {
            Move(target);
            
            yield return _wait;
        }

        _vampirism.GetConfirmationOfEmptiness(true);

        StartCoroutine(WaitForIncrease(_slider.maxValue));
    }

    private IEnumerator WaitForIncrease(float target)
    {
        while (_slider.value < target)
        {
            Move(target);

            yield return _wait;
        }

        _vampirism.GetConfirmationOfEmptiness(false);
    }
}