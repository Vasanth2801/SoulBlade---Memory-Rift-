using UnityEngine;

public static class Stats 
{
    public static int MaxHealth(Attributes attributes) => attributes.vitality * 5 + attributes.power * 2;
    public static int AttackDamage(Attributes attributes) => attributes.power * 2 + attributes.agility;
    public static int SpellDamage(Attributes attributes) => attributes.focus * 5;
    public static float CritChance(Attributes attributes) => attributes.agility * .05f;
}
