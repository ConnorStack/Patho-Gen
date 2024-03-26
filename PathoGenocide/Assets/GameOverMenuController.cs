using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    public GameObject gameOverMenuUI;

    void Start()
    {
        gameOverMenuUI.SetActive(false);
    }

    void Update()
    {

    }

    public void ShowGameOverMenu()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
