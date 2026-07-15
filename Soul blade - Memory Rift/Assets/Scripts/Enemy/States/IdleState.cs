using UnityEngine;

public class IdleState : State
{
    public IdleState(Enemy enemy) : base(enemy) { }

    private Transform target;
    protected override string AnimBoolName => "isIdling";

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = Vector2.zero;
    }

    public override void FixedUpdate()
    {
        // 1. Check for target 
        target = senses.GetChaseTarget();

        if (!target)
        {
            stateMachine.ChangeState(new PatrolState(enemy));
            return;
        }

        enemy.FaceTarget(target);

        //2. Check if we have reached our target 
        float distance = Mathf.Abs(target.position.x - enemy.transform.position.x);
        if (distance <= config.turnThreshold)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        //3. Check for Obstacles 
        if (senses.IsHittingWall() || senses.IsAtCliff())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        //4.We have a target, Chase it 
        stateMachine.ChangeState(new ChaseState(enemy));
    }
}