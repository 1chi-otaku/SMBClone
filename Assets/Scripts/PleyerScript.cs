using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveForce = 15f;
    public float maxSpeed = 8f;
    public float stopDrag = 5f;
    public float moveDrag = 0.5f;

    public LayerMask groundLayer;
    public float groundCheckDistance = 0.6f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = moveDrag;
    }

    public void Move(Vector3 direction)
    {
        if (IsGrounded())
        {
            if (direction.magnitude > 0.01f)
            {
                // ????????? ????, ???? ???????? ?? ????????? maxSpeed
                if (rb.linearVelocity.magnitude < maxSpeed)
                {
                    rb.AddForce(direction * moveForce, ForceMode.Acceleration);
                }

                // ???? ????? ????????? � ?????? ??????? drag
                rb.linearDamping = moveDrag;
            }
            else
            {
                // ???? ????? ?? ????????? � ??????????? drag, ????? ????????????
                rb.linearDamping = stopDrag;
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}
