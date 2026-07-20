using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerState currentState;

    public PlayerIdleState idleState;
    public PlayerJumpState jumpState;
    public PlayerMoveState moveState;
    public PlayerCrouchState crouchState;
    public PlayerSlideState slideState;
    public PlayerAttackState attackState;
    public PlayerDamagedState damagedState;
    public PlayerDeathState deathState;
    public PlayerSpellCastState spellCastState;

    [Header("Movement Settings")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public int facingDirection = 1;

    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float jumpCutMultiplier = 0.5f;
    [SerializeField] private float normalGravity;
    [SerializeField] private float jumpGravity;
    [SerializeField] private float fallGravity;

    [Header("Slide Settings")]
    public float slideDuration = 0.6f;
    public float slideSpeed = 12f;
    public float slideStopDuration = 0.15f;

    private bool isSliding;

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
    public bool isGrounded;
 
    [Header("Unity Components References")]
    public Rigidbody2D rb;
    public Animator anim;
    [SerializeField] private PlayerInput player;
    [SerializeField] private CapsuleCollider2D playerCollider;

    [Header("Core Component References")]
    public Combat combat;
    public Damage damage;
    public Magic magic;
    public Health health;

    [Header("Inputs")]
    public Vector2 moveInput;
    public bool jumpPressed;
    public bool jumpReleased;
    public bool runPressed;
    public bool attackPressed;
    public bool spellCastPressed;

    private void Awake()
    {
        idleState = new PlayerIdleState(this);
        jumpState = new PlayerJumpState(this);
        moveState = new PlayerMoveState(this);
        crouchState = new PlayerCrouchState(this);
        slideState = new PlayerSlideState(this);
        attackState = new PlayerAttackState(this);
        damagedState = new PlayerDamagedState(this);
        deathState = new PlayerDeathState(this);
        spellCastState = new PlayerSpellCastState(this);
    }

    private void Start()
    {
        rb.gravityScale = normalGravity;

        ChangeState(idleState);
    }

    private void Update()
    {
        currentState.Update();
        if(!isSliding)
        {
            Flip();
        }
      
        HandleAnimations();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
        CheckGrounded();
        ApplyGravity();
    }

    public void ChangeState(PlayerState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter();
    }

    public void SetColliderNormal()
    {
        playerCollider.size = new Vector2(playerCollider.size.x, normalHieght);
        playerCollider.offset = normalOffSet;
    }

    public void SetColliderSlide()
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

    public void OnAttack(InputValue value)
    {
        attackPressed = value.isPressed;
    }

    public void OnSpellCast(InputValue value)
    {
        spellCastPressed = value.isPressed;
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public bool CheckCieling()
    {
        return Physics2D.OverlapCircle(headCheck.position, headCheckRadius, groundLayer);
    }

    public void ApplyGravity()
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
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    void Flip()
    {
        if(currentState == deathState)
        {
            return;
        }

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

    public void AnimationFinsihed()
    {
        currentState.AnimationFinished();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(headCheck.position, headCheckRadius);
    }
}