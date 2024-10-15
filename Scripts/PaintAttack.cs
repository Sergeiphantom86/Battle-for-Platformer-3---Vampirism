using System.Collections;
using UnityEngine;

public class PaintAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Coroutine _coroutine;

    public bool IsOpaque { get; private set; }

    private void Awake()
    {
        IsOpaque = true;
    }

    public void ChangeColor()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        if (TryGetPresenceOfSpriteRenderer())
        {
            _coroutine = StartCoroutine(ReturnDefaultColor());
        }
    }

    public void TryDelete()
    {
        StartCoroutine(DelayBeforeDeleting());
    }

    private void ChangeAlpha(float amountTransparencyIncreases = 0.01f)
    {
        _spriteRenderer.color = new Color(1, 1, 1, _spriteRenderer.color.a - amountTransparencyIncreases);
    }

    private bool TryGetPresenceOfSpriteRenderer()
    {
        if (_spriteRenderer != null)
        {
            return true;
        }

        return false;
    }

    private IEnumerator ReturnDefaultColor()
    {
        float colorChangeDelay = 0.01f;

        WaitForSeconds wait = new(colorChangeDelay);

        if (TryGetPresenceOfSpriteRenderer())
        {
            _spriteRenderer.color = Color.red;

            yield return wait;

            _spriteRenderer.color = Color.white;
        }
    }

    private IEnumerator DelayBeforeDeleting()
    {
        float delay = 0.05f;

        WaitForSeconds wait = new(delay);

        if (TryGetPresenceOfSpriteRenderer())
        {
            while (_spriteRenderer.color.a > 0)
            {
                yield return wait;

                ChangeAlpha();
            }

            if (_spriteRenderer.color.a <= 0)
            {
                IsOpaque = false;
            }
        }
    }
}