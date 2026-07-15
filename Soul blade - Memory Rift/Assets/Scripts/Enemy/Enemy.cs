using UnityEngine;

public class Enemy : MonoBehaviour
{
   public Rigidbody2D RB { get; private set; }
    public StateMachine StateMachine { get; private set; }

   private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        StateMachine = new StateMachine();
    }

    private void Update() => StateMachine.CurrentState?.Update();
    private void FixedUpdate() => StateMachine.CurrentState?.FixedUpdate();
}