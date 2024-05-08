using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryPopup : MonoBehaviour
{
    public GameObject popupPanel; // Assign the panel in the inspector

    void Start()
    {
        popupPanel.SetActive(false); // Start with the popup hidden
    }

    // Call this method to show the popup
    public void ShowVictoryPopup()
    {
        popupPanel.SetActive(true);
    }

    // Call this method from the button's OnClick() event
    public void OnReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu or handle as needed
    }

    // Optionally, add a method to close the popup if not leaving the scene
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}