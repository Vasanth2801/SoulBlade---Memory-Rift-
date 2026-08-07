using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SaveManager : MonoBehaviour
{
    private readonly List<IDataPersistance> persistantObjects = new();

    private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    private void Awake()
    {
        ServiceLocator.Register(this);
    }

    private void Start()
    {
        StartCoroutine(LoadAfterInit());
    }

    private IEnumerator LoadAfterInit()
    {
        yield return null;
        LoadGame();
    }

    public void Register(IDataPersistance persistanceObj)
    {
        persistantObjects.Add(persistanceObj);
    }

    public void SaveGame()
    {
        SaveData saveData = new();

        foreach (var obj in persistantObjects)
        {
            obj.SaveData(saveData);
        }


        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json); 

        Debug.Log($"[SaveManager] Game saved to {SavePath}");
    }

    public void LoadGame()
    {
        if(!File.Exists(SavePath))
        {
            return;
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        foreach(var obj in persistantObjects)
        {
            obj.LoadData(data);
        }
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }
}
