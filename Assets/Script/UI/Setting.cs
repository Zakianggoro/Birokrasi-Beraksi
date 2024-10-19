using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Slider volumeSlider; // Assign this in the Inspector
    public Slider brightnessSlider; // Assign this in the Inspector
    public AudioSource audioSource; // Assign the AudioSource component
    public Image brightnessOverlay; // Assign an overlay image for brightness control

    private void Start()
    {
        // Initialize sliders with saved values or defaults
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f); // Default volume to 100%
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1f); // Default brightness to 100%

        // Apply initial settings
        SetVolume(volumeSlider.value);
        SetBrightness(brightnessSlider.value);

        // Add listeners to detect slider value changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    // Method to control volume
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume); // Save volume setting
    }

    // Method to control brightness
    public void SetBrightness(float brightness)
    {
        Color overlayColor = brightnessOverlay.color;
        overlayColor.a = 1f - brightness; // Adjust alpha to change screen brightness
        brightnessOverlay.color = overlayColor;
        PlayerPrefs.SetFloat("Brightness", brightness); // Save brightness setting
    }
}