using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    bool isFacingRight = true;

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
    bool isGrounded;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f; 

    [Header("WallCheck")] 
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask wallLayer;

    [Header("WallMovement")]
    public float wallSlideSpeed = 2;
    bool isWallSliding; 
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * movespeed, rb.linearVelocity.y);
        
        log();
        GroundCheck();
        processGravity();
        processWallSlide();
        Flip();
    }

    private void Gravity()
    {
       rb.gravityScale = baseGravity * fallSpeedMultiplier;
       rb.linearVelocity = new Vector2(rb.linearVelocity.x , Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
    }

    private void log()
    {
         Debug.Log(
        "Grounded = " + isGrounded +
        " | Wall = " + wallCheck() +
        " | WallSlide = " + isWallSliding +
        " | VelocityY = " + rb.linearVelocity.y
        );
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
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }

    }

    public bool wallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position , wallCheckSize, 0, wallLayer);
    }

    public void processGravity()
    {
        if(rb.linearVelocity.y < 0)
        {
            Gravity();
        } else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void processWallSlide()
    {
        if (!isGrounded & wallCheck() & horizontalMovement !=0)
        {
            isWallSliding = true;
            rb.gravityScale =0;

            if(rb.linearVelocity.y > -wallSlideSpeed)
            {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, - wallSlideSpeed);
            }
        } else
        {
            isWallSliding = false;
        }
    }

    private void Flip()
    {
        if(isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    }
}
