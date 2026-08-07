using UnityEngine;

public class PlayerPersistance : MonoBehaviour,IDataPersistance
{
    
    void Start()
    {
        SaveManager saveManager = ServiceLocator.Get<SaveManager>();
        saveManager.Register(this);
    }
     
    public void SaveData(SaveData saveData)
    {
        saveData.playerPosition = transform.position;
    }

    public void LoadData(SaveData saveData)
    {
        transform.position = saveData.playerPosition;
    }
}