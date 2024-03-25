using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    public GameObject gameOverMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowGameOverMenu()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze the game
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Unfreeze the game
        SceneManager.LoadScene("MainMenu");
    }
}
