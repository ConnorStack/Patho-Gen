using UnityEngine;
using UnityEngine.UI;  // Required for working with UI elements like Slider

public class VolumeControl : MonoBehaviour
{
    private Slider volumeSlider;

    void Start()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f); // Default to 0.5 if not set
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);  // Save volume setting
    }
}