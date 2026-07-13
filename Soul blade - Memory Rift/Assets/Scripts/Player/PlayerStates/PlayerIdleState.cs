using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();
        anim.SetBool("isIdle", true);
    }

    public override void Update()
    {
        base.Update();

        if(player.jumpPressed)
        {
            player.jumpPressed = false;
            player.ChangeState(player.jumpState);
        }
    }

    public override void Exit()
    {   
        base.Exit();
        anim.SetBool("isIdle", false);
    }
}