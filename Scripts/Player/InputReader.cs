using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    public const string Fire2 = nameof(Fire2);
    public const string Jump = nameof(Jump);

    private bool _isJump;
    public event Action ButtonPressed;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetButtonDown(Jump))
            _isJump = true;
        if (Input.GetButtonDown(Fire2))
            ButtonPressed?.Invoke();
    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}