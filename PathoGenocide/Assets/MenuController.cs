using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Open Game");
        SceneManager.LoadScene("GameScene"); // Replace "GameSceneName" with the actual name of your game scene
    }

    public void OpenInstructions()
    {
        // You can load an instructions scene or simply enable a UI panel that contains your game instructions.
        // For example, to load an Instructions scene: SceneManager.LoadScene("InstructionsSceneName");
        Debug.Log("Open instructions");
    }

    public void QuitGame()
    {
        // If running in the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
