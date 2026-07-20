using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Heal")]
public class HealSpellSO : SpellSO
{
    [Header("Heal Settings")]
    public int healAmount = 10;
    public GameObject healFX;

    public override void Cast(Player player)
    {
        GameObject newHealFX = Instantiate(healFX, player.transform.position, Quaternion.identity);
        Destroy(newHealFX, 2);

        player.health.ChangeHealth(healAmount, player.transform.position);
    }
}