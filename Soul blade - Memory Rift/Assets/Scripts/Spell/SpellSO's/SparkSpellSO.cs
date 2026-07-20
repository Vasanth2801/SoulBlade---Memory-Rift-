using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Spark")]
public class SparkSpellSO : SpellSO
{
    [Header("Spark Settings")]
    public int damage = 5;
    public float radius = 5;
    public GameObject sparkFX;
    public LayerMask enemyLayer;

    public override void Cast(Player player)
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(player.transform.position, radius, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            Health health = enemy.GetComponent<Health>();

            if (health != null)
            {
                health.ChangeHealth(-damage, enemy.transform.position);
            }

            if (sparkFX != null)
            {
                GameObject newFX = Instantiate(sparkFX, enemy.transform.position, Quaternion.identity);
                Destroy(newFX, 2);
            }
        }
    }
}