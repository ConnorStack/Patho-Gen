using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject instructionsPanel;

    void Start()
    {
        instructionsPanel.SetActive(false);  // Hide the panel initially
    }
    public void PlayGame()
    {
        Debug.Log("Open Game");
        SceneManager.LoadScene("GameScene");
    }

    public void OpenInstructions()
    {
        Debug.Log("Open instructions");
        instructionsPanel.SetActive(true);  // Show the instructions panel
    }

    public void CloseInstructions()
    {
        Debug.Log("Close instructions");
        instructionsPanel.SetActive(false);  // Hide the instructions panel
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
