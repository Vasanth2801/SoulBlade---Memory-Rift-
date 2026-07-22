using UnityEngine;
using UnityEngine.SceneManagement;

public static class BootStrapLoader
{
    private const string BootStrapScene = "BootStrap";
    private const string MainMenuScene = "MainMenu";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        EnsureBootstrapLoaded(SceneManager.GetActiveScene());
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnsureBootstrapLoaded(scene);
    }

    private static void EnsureBootstrapLoaded(Scene scene)
    {
        if(scene.name == MainMenuScene)
        {
            return;
        }

        if(!SceneManager.GetSceneByName(BootStrapScene).isLoaded)
        {
            SceneManager.LoadSceneAsync(BootStrapScene, LoadSceneMode.Additive);
        }
    }
}