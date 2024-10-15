using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // To change scenes

public class SlideAndChangeScene : MonoBehaviour
{
    public Slider slider; // Assign your slider in the Inspector
    public float requiredValue = 100f; // The value the player must reach to avoid reset
    public float resetSpeed = 2f; // Speed at which the slider returns to 0
    public Image blinkImage; // UI Image that will act as the blink effect (assign in Inspector)
    public float blinkSpeed = 1f; // Speed of the blink effect
    public string nextScene; // Name of the scene to load after blinking

    private bool dragging = false; // To track if the player is dragging the slider
    private bool isBlinking = false; // To track if the blinking animation is playing

    void Start()
    {
        // Ensure the slider starts at 0 and the blink image is fully transparent
        slider.value = 0;
        Color blinkColor = blinkImage.color;
        blinkColor.a = 0f;
        blinkImage.color = blinkColor;
    }

    void Update()
    {
        // Check if the player is dragging the slider
        if (Input.GetMouseButton(0))
        {
            dragging = true;
        }
        else if (dragging && !Input.GetMouseButton(0))
        {
            // Player released the drag
            dragging = false;

            // If the slider wasn't dragged fully to the required value, reset it
            if (slider.value < requiredValue)
            {
                StartCoroutine(ResetSlider());
            }
            else if (!isBlinking)
            {
                // If the slider reached the required value, start the blink effect and change scene
                StartCoroutine(BlinkAndChangeScene());
            }
        }
    }

    // Coroutine to smoothly reset the slider back to 0
    IEnumerator ResetSlider()
    {
        while (slider.value > 0)
        {
            slider.value = Mathf.MoveTowards(slider.value, 0, resetSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }
    }

    // Coroutine to handle blinking effect before changing the scene
    IEnumerator BlinkAndChangeScene()
    {
        isBlinking = true;
        Color blinkColor = blinkImage.color;

        // Fade the blink image in (increase alpha)
        while (blinkColor.a < 1f)
        {
            blinkColor.a = Mathf.MoveTowards(blinkColor.a, 1f, blinkSpeed * Time.deltaTime);
            blinkImage.color = blinkColor;
            yield return null; // Wait for the next frame
        }

        // Wait for a brief moment with the screen fully covered
        yield return new WaitForSeconds(0.5f);

        // Load the next scene
        SceneManager.LoadScene(nextScene);
    }
}