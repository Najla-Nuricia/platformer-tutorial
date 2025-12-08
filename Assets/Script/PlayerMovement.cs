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

    //wall jump
    bool isWallJumping;
    float wallJumpiDirection;
    float wallJumpTime = 0.5f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f,10f);
    void Update()
    {
        GroundCheck();
        processGravity();
        processWallSlide();
        ProcessWallJump();
    

        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * movespeed, rb.linearVelocity.y);
            Flip();
        }
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

        //wall jump
         if(context.performed && wallJumpTimer > 0f){
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpiDirection * wallJumpPower.x , wallJumpPower.y);
            wallJumpTimer =0;

            //force flip 
            if(transform.localScale.x != wallJumpiDirection)
            {
                FlipProcess();
            }

            Invoke(nameof(cancelWallJump), wallJumpTime + 0.1f);
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

    private void ProcessWallJump()
    {
        if (isWallSliding)
        {
            isWallJumping=false;
            wallJumpiDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(cancelWallJump));
        } else if(wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void cancelWallJump()
    {
        isWallJumping = false;
    }

    private void FlipProcess(){
        isFacingRight = !isFacingRight;
        Vector3 ls = transform.localScale;
        ls.x *= -1f;
        transform.localScale = ls;
    }

    private void Flip()
    {
        if(isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            FlipProcess();
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
