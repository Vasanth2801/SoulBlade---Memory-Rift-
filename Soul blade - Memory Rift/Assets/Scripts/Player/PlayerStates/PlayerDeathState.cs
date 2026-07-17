using UnityEngine;

public class PlayerDeathState : PlayerState
{
    private float knockbackVelocity;
    private float knockbackDuration;
    private bool isTimeSlow;

    public PlayerDeathState(Player player) : base(player) { }

    public void SetParameters(int knockbackDirection)
    {
        knockbackVelocity = knockbackDirection * damage.knockbackForce;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetBool("isDead", true);
        Time.timeScale = 0.3f;
        isTimeSlow = true;

        knockbackDuration = damage.knockbackDuration;
        rb.linearVelocity = new Vector2(knockbackVelocity, rb.linearVelocity.y);
    }

    public override void FixedUpdate()
    {
        knockbackDuration -= Time.fixedDeltaTime;

        if (knockbackDuration <= 0)
        {
            if(isTimeSlow)
            {
                Time.timeScale = 1f;
                isTimeSlow = false;
            }
            if (player.isGrounded)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    public override void Exit()
    {
        base.Enter();
        anim.SetBool("isDead", false);
    }
}
