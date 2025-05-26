using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject ContinueButton;
    public void PlayGame()
    {
        SceneManager.LoadScene("TutorialChamber1");
    }

    public void ContinueGame()
    {
        Debug.Log(LevelManager.Instance.lastPlayedLevelName);
        SceneManager.LoadScene(LevelManager.Instance.lastPlayedLevelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (LevelManager.Instance.lastPlayedLevelName != "")
            ContinueButton.SetActive(true);
    }
}
