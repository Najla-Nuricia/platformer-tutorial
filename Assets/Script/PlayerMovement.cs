using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Movement")]
    public float movespeed = 5f;
    private float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJump =2;
    int jumpsRemaining;

    [Header("GroundCheck")] 
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f; 

    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * movespeed, rb.linearVelocity.y);
        GroundCheck();
    }

    private void Gravity()
    {
       rb.gravityScale = baseGravity * fallSpeedMultiplier;
       rb.linearVelocity = new Vector2(rb.linearVelocity.x , Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontalMovement = input.x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
        if (context.performed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            jumpsRemaining--;
        }
        else if (context.canceled)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            jumpsRemaining--;
        }
        }
    }

    public void GroundCheck()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position , groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJump;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
