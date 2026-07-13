using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private int facingDirection = 1;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpCutMultiplier = 0.5f;
    [SerializeField] private float normalGravity;
    [SerializeField] private float jumpGravity;
    [SerializeField] private float fallGravity;

    [Header("Slide Settings")]
    [SerializeField] private float slideDuration = 0.6f;
    [SerializeField] private float slideSpeed = 12f;
    [SerializeField] private float slideStopDuration = 0.15f;

    private bool isSliding;
    private bool slideInputLocked;
    private float slideTimer;
    private float slideStopTimer;

    [SerializeField] private float slideHieght;
    [SerializeField] private Vector2 slideOffSet;
    [SerializeField] private float normalHieght;
    [SerializeField] private Vector2 normalOffSet;

    [Header("Crouch Settings")]
    [SerializeField] private Transform headCheck;
    [SerializeField] private float headCheckRadius = 0.2f;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
 
    [Header("Unity Components References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerInput player;
    [SerializeField] private Animator anim;
    [SerializeField] private CapsuleCollider2D playerCollider;

    [Header("Inputs")]
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private bool jumpPressed;
    [SerializeField] private bool jumpReleased;
    [SerializeField] private bool runPressed;

    private void Start()
    {
        rb.gravityScale = normalGravity;
    }

    private void Update()
    {
        TryStandUp();
        if(!isSliding)
        {
            Flip();
        }
      
        HandleAnimations();
        HandleSlide();
    }

    private void FixedUpdate()
    {
        if (!isSliding)
        {
            HandleMovement();
        }
        HandleJump();
        CheckGrounded();
        ApplyGravity();
    }

    void HandleMovement()
    {
        float currentSpeed = runPressed ? runSpeed : walkSpeed;
        float targetSpeed = moveInput.x * currentSpeed;
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
    }

    void HandleJump()
    {
        if(jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
            jumpReleased = false;
        }
        else if(jumpReleased)
        {
            if (rb.linearVelocity.y > 0.1f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
            }
            jumpReleased = false;
        }
    }

    void HandleSlide()
    {
        if(isSliding)
        {
            slideTimer -= Time.deltaTime;
            rb.linearVelocity = new Vector2(slideSpeed * facingDirection, rb.linearVelocity.y);

            //We are done sliding 
            if(slideTimer <= 0)
            {
                isSliding = false;
                slideStopTimer = slideStopDuration;
                TryStandUp();
            }
        }

        if(slideStopTimer > 0)
        {
            slideStopTimer -= Time.deltaTime;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        if(isGrounded && runPressed && moveInput.y < -0.1f && !isSliding && !slideInputLocked)
        {
            isSliding = true;
            slideInputLocked = true;
            slideTimer = slideDuration;
            SetColliderSlide();
        }

        if(slideStopTimer < 0 && moveInput.y >= -0.1f)
        {
            slideInputLocked = false;
        }
    }

    void TryStandUp()
    {
        if(isSliding)
        {
            anim.SetBool("isCrouching", false);
            return;
        }
       
        bool shouldCrouch = moveInput.y <= -0.1f || Physics2D.OverlapCircle(headCheck.position, headCheckRadius, groundLayer);

        if (!shouldCrouch)
        {
            SetColliderNormal();
            anim.SetBool("isCrouching", false);
        }
        else
        {
            SetColliderSlide();
            anim.SetBool("isCrouching", true);
        }
    }

    void SetColliderNormal()
    {
        playerCollider.size = new Vector2(playerCollider.size.x, normalHieght);
        playerCollider.offset = normalOffSet;
    }

    void SetColliderSlide()
    {
        playerCollider.size = new Vector2(playerCollider.size.x, slideHieght);
        playerCollider.offset = slideOffSet;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if(value.isPressed && isGrounded)
        {
            jumpPressed = true;
            jumpReleased = false;
        }
        else  // If we still going up
        {
           jumpReleased = true;
        }
    }

    public void OnRun(InputValue value)
    {
        runPressed = value.isPressed;
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void ApplyGravity()
    {
        if(rb.linearVelocity.y < -0.1f)
        {
            rb.gravityScale = fallGravity;
        }
        else if(rb.linearVelocity.y > 0.1f)
        {
            rb.gravityScale = jumpGravity;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
    }

    void HandleAnimations()
    {
        bool isCrouching = anim.GetBool("isCrouching");

        anim.SetBool("isJumping", rb.linearVelocity.y > 0.1f);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        anim.SetBool("isSliding", isSliding);

        bool isMoving = Mathf.Abs(moveInput.x) > 0.1f && isGrounded;
        anim.SetBool("isIdle", !isMoving && isGrounded && !isSliding && !isCrouching);
        anim.SetBool("isWalking", isMoving && !runPressed && !isSliding && !isCrouching);
        anim.SetBool("isRunning", isMoving && runPressed && !isSliding && !isCrouching);
    }

    void Flip()
    {
        if(moveInput.x > 0.1f)
        {
            facingDirection = 1;
        }
        else if(moveInput.x < -0.1f)
        {
            facingDirection = -1;
        }

        transform.localScale = new Vector3(facingDirection, 1f, 1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(headCheck.position, headCheckRadius);
    }
}