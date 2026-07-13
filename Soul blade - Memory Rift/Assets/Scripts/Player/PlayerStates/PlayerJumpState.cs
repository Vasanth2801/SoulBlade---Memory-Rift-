using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player) : base(player) { }
    
    public override void Enter()
    {
        base.Enter();
        anim.SetBool("isJumping", true);

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);

        JumpPressed = false;
        JumpReleased = false;
    }

    public override void Update()
    {
        base.Update();

        if(player.isGrounded && rb.linearVelocity.y <= 0.1f)
        {
            player.ChangeState(player.idleState);
        }
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.ApplyGravity();

        if(JumpReleased && rb.linearVelocity.y >= 0.1f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * player.jumpCutMultiplier);
            JumpReleased = false;
        }

        float speed = RunPressed ? player.runSpeed : player.walkSpeed;
        rb.linearVelocity = new Vector2(speed * player.facingDirection, rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool("isJumping", false);
    }
}


//void HandleJump()
//{
//    if (jumpPressed && isGrounded)
//    {
//        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
//        jumpPressed = false;
//        jumpReleased = false;
//    }
//    else if (jumpReleased)
//    {
//        if (rb.linearVelocity.y > 0.1f)
//        {
//            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
//        }
//        jumpReleased = false;
//    }
//}