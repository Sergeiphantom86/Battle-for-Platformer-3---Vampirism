using UnityEngine;

public class Flipper : MonoBehaviour
{
    private bool _facingRight = true;

    public void FlipCharacter(float move)
    {
        if (move < 0 && _facingRight)
        {
            Flip();
        }
        else if (move > 0 && _facingRight == false)
        {
            Flip();
        }
    }

    public void Flip()
    {
        _facingRight = !_facingRight;

        float turn = 180f;

        transform.Rotate(0, turn, 0);
    }
}