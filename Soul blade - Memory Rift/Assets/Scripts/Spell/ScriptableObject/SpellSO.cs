using UnityEngine;

public abstract class SpellSO : ScriptableObject
{
    [Header("General")]
    public string spellName;
    public float coolDown;
    public Sprite icon;

    public abstract void Cast(Player player);
}