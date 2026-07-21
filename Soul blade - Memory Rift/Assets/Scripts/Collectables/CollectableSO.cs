using UnityEngine;

public abstract class CollectableSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public abstract void Collect(Player player);
}