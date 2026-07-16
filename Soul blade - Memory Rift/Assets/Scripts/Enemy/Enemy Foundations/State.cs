using UnityEngine;

public class State 
{
    protected virtual string AnimBoolName => null;

    protected Rigidbody2D rb;
    protected EnemyConfig config;
    protected EnemySenses senses;
    protected Enemy enemy;
    protected Animator anim;
    protected StateMachine stateMachine;

    protected State(Enemy enemy)
    {
        rb = enemy.RB;
        config = enemy.Config;
        senses = enemy.Senses;
        this.enemy = enemy;
        anim = enemy.Anim;
        stateMachine = enemy.StateMachine;
    }

    public virtual void Enter() 
    {
        if(!string.IsNullOrEmpty(AnimBoolName))
        {
            anim.SetBool(AnimBoolName, true);
        }
    }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() 
    {
        if (!string.IsNullOrEmpty(AnimBoolName))
        {
            anim.SetBool(AnimBoolName, false);
        }
    }
}