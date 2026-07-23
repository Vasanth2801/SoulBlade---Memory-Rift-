using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    public int FacingDirection { get; private set; } = 1;
    public Transform CurrentTarget {  get; set; }

    //Components
    public EnemyConfig Config;
    public Rigidbody2D RB {  get; private set; }
    public Animator Anim {  get; private set; }
    public EnemySenses Senses { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public Enemy_Combat Combat { get; private set; }

    //Persistance
    private WorldState worldState;
    private PersistentGuid guid;
    private bool isDefeated;

    private void Awake()
    {
        guid = GetComponent<PersistentGuid>();
        RB = GetComponent<Rigidbody2D>();
        StateMachine = new StateMachine();
        Senses = GetComponent<EnemySenses>();
        Anim = GetComponent<Animator>();
        Combat = GetComponent<Enemy_Combat>();
    }

    private void Start()
    {
        // Persistance

        worldState = ServiceLocator.Get<WorldState>();

        if(worldState.defeatedEnemies.Contains(guid.Guid))
        {
            Destroy(gameObject);
            return;
        }

        StateMachine.Initialize(new PatrolState(this));
    }

    private void Update() => StateMachine.CurrentState?.Update();
    private void FixedUpdate() => StateMachine.CurrentState?.FixedUpdate();
    public void OnAnimationFinished() => StateMachine.CurrentState?.OnAnimationFinished();

    public void FaceTarget(Transform target)
    {
        float offSet = target.position.x - transform.position.x;

        int direction = offSet > 0 ? 1 : -1;

        if(direction != FacingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        FacingDirection *= -1;

        Vector3 scale = transform.localScale;
        scale.x = FacingDirection;
        transform.localScale = scale;
    }

    public void Die()
    {
        if(isDefeated)
        {
            return;
        }

        worldState.defeatedEnemies.Add(guid.Guid);
    }
}
