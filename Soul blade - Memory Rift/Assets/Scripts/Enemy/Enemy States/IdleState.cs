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
        enemy.CurrentTarget = target;

        if (!target)
        {
            stateMachine.ChangeState(new PatrolState(enemy));
            return;
        }

        enemy.FaceTarget(target);

        //2. Check if we can attack
        if (senses.IsInMeleeRange(target) && combat.CanMeleeAttack())
        {
            stateMachine.ChangeState(new MeleeAttackState(enemy));
            return;
        }

        //3.Check if we can RangedAttack
        if (senses.IsInShootingRange(target) && combat.CanRangeAttack())
        {
            stateMachine.ChangeState(new RangedAttackState(enemy));
            return;
        }

        // 4. Check if we reached our target 
        float distance = target.position.x - enemy.transform.position.x;
        if (Mathf.Abs(distance) <= config.turnThershold)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // 5. Check for obsctales 
        if (senses.IsHittingWall() || senses.IsAtCliff())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
       
        //6. Change the State to the next thing 
        stateMachine.ChangeState(new ChaseState(enemy));
    }
}