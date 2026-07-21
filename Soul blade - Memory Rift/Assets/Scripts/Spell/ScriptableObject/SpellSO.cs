using UnityEngine;

public abstract class SpellSO : CollectableSO
{
    [Header("General")]
    public float coolDown;

    public override void Collect(Player player)
    {
        player.magic.LearnSpell(this);
    }

    public abstract void Cast(Player player);
}