using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;

    public bool IsGround(float checkRadius = 0.25f)
    {
        return Physics2D.OverlapCircle(_groundCheck.position, checkRadius, _whatIsGround);
    }
}