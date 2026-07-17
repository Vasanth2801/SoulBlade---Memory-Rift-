using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Enemy enemy;

    [SerializeField] private GameObject[] deathParts;
    [SerializeField] private float spawnForce = 5f;
    [SerializeField] private float torque = 5f;
    [SerializeField] private float lifeTime = 2f;

    private void OnEnable()
    {
        health.onDamaged += HandleDamage;
        health.onDeath += HandleDeath;
    }

    private void OnDisable()
    {
        health.onDamaged -= HandleDamage;
        health.onDeath += HandleDeath;
    }

    void HandleDamage(Vector2 sourcePosition)
    {
        int knockbackDirection = 0;
        knockbackDirection = transform.position.x > sourcePosition.x ? 1 : -1; 

        enemy.StateMachine.ChangeState(new DamagedState(enemy, knockbackDirection));
    }

    void HandleDeath(Vector2 soucePosition)
    {
        foreach(GameObject prefab in deathParts)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(.5f, 1f)).normalized;
            GameObject part = Instantiate(prefab, transform.position, rotation);

            Rigidbody2D rb = part.GetComponent<Rigidbody2D>();
            Vector2 randomDirection = new Vector2(Random.Range(-1, 1), Random.Range(0.5f, 1f)).normalized;
            rb.linearVelocity = randomDirection * spawnForce;
            rb.AddTorque(Random.Range(-torque, torque), ForceMode2D.Impulse);

            Destroy(part, lifeTime);
        }

        Destroy(gameObject);
    }
}