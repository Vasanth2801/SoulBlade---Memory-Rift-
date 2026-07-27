using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Heal")]
public class HealSpellSO : SpellSO
{
    [Header("Heal Settings")]
    public int healAmount = 10;
    public GameObject healFX;

    public override void Cast(Player player, int spellPower)
    {
        GameObject newHealFX = Instantiate(healFX, player.transform.position, Quaternion.identity);
        Destroy(newHealFX, 2);

        float spellModifier = 1f + (spellPower / 30f);
        int realAmount = Mathf.RoundToInt(healAmount + spellModifier);   

        player.health.ChangeHealth(realAmount, player.transform.position);
    }
}