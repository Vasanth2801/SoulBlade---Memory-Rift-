using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    [Header("References to player progression Manager")]
    public ProgressionManager ProgressionManager;
        
    private void Start()
    {
        ApplyAllStats();
    }

    private void OnEnable() => ProgressionManager.OnStatsChanged += ApplyAllStats;
    private void OnDisable() => ProgressionManager.OnStatsChanged -= ApplyAllStats;

    private void ApplyAllStats()
    {
        //Apply HEalth
        //Apply Damage
        //Apply Spell 
        Debug.Log("All Stats was changed");
    }
}