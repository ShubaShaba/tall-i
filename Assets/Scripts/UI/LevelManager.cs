using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public string currentLevelName { get; private set; }
    public string lastPlayedLevelName { get; private set; }

    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist between scenes

        currentLevelName = SceneManager.GetActiveScene().name;
        lastPlayedLevelName = "";
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if (currentLevelName != "MainMenu")
            lastPlayedLevelName = currentLevelName;
        currentLevelName = newScene.name;
        Debug.Log("Level changed to: " + currentLevelName + " from " + lastPlayedLevelName);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            SceneManager.activeSceneChanged -= OnSceneChanged;
    }
}
