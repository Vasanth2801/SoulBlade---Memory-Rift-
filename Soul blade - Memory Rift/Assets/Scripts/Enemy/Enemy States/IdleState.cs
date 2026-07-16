using UnityEngine;

public class IdleState : State
{
    private Transform target;
    protected override string AnimBoolName => "isIdling";

    public IdleState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = Vector2.zero;
    }

    public override void FixedUpdate()
    {
        //1. Check for the target 
        target = senses.GetTarget();

        if (!target)
        {
            stateMachine.ChangeState(new PatrolState(enemy));
            return;
        }

        enemy.FaceTarget(target);

        // 2. Check if we reached our target 
        float distance = target.position.x - enemy.transform.position.x;
        // use absolute horizontal distance so this works when target is left or right
        if (Mathf.Abs(distance) <= config.turnThershold)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // 3. Check for obsctales 
        if (senses.IsHittingWall() || senses.IsAtCliff())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
       
        //4. Change the State to the next thing 
        stateMachine.ChangeState(new ChaseState(enemy));
    }
}