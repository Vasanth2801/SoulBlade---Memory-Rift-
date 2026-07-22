using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Security.Cryptography;

public class RoomTransitionManager : MonoBehaviour
{

    private string currentRoom = "";

    private void Start()
    {
        EnterRoom("", ""); 
    }

    public void EnterRoom(string sceneName, string spawnID)
    {
        StartCoroutine(Transition(sceneName, spawnID));
    }

    private IEnumerator Transition(string sceneName, string spawnID)
    {
        if (!string.IsNullOrEmpty(currentRoom))
        {
            yield return SceneManager.UnloadSceneAsync(currentRoom);
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        Scene newScene = SceneManager.GetSceneByName(sceneName);

        if(newScene.IsValid())
        {
            SceneManager.SetActiveScene(newScene);
        }

        currentRoom = SceneManager.GetActiveScene().name;
        SetupRoom(spawnID);
    }

    private void SetupRoom(string spawnID)
    {
        SpawnPoint[] spawns = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
        SpawnPoint spawnToUse = spawns[0];

        if(!string.IsNullOrEmpty(spawnID))
        {
            foreach(SpawnPoint spawn in spawns)
            {
                if(spawn.spawnID == spawnID)
                {
                    spawnToUse = spawn;
                    transform.position = spawnToUse.transform.position;
                    break;
                }
            }
        }
    }
}