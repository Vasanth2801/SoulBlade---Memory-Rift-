using UnityEngine;

public abstract class State
{
    protected Rigidbody2D rb;
    protected EnemyConfig config;

    protected State(Enemy enemy)
    {
        rb = enemy.RB;
        config = enemy.Config;
    }


    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
}