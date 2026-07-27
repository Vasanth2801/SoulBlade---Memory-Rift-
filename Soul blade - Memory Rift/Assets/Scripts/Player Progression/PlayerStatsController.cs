using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    [Header("References to player progression Manager")]
    public ProgressionManager ProgressionManager;
    public Health health;
    public Combat combat;

    private Attributes BaseAttributes => ProgressionManager.baseAttributes;
        
    private void Start()
    {
        ApplyAllStats();
    }

    private void OnEnable() => ProgressionManager.OnStatsChanged += ApplyAllStats;
    private void OnDisable() => ProgressionManager.OnStatsChanged -= ApplyAllStats;

    private void ApplyAllStats()
    {
        ApplyHealthStats();
        //Apply Damage
        //Apply Spell 
        Debug.Log("All Stats was changed");
    }

    void ApplyHealthStats()
    {
        health.ChangeMaxHealth(Stats.MaxHealth(BaseAttributes));
    }
}