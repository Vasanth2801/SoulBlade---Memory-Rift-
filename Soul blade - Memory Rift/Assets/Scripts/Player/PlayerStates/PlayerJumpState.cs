using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player) : base(player) { }
    
    public override void Enter()
    {
        base.Enter();
        anim.SetBool("isJumping", true);

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);
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