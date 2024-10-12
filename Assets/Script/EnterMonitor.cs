using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterMonitor : MonoBehaviour
{
    [SerializeField] private GameObject panel;    // The panel to deactivate
    [SerializeField] private string targetScene;  // The name of the scene to load after 3 seconds
    [SerializeField] private float delay = 3f;    // Delay before changing the scene (3 seconds)

    // Method to be called when the button is pressed
    public void OnButtonPress()
    {
        // Deactivate the panel
        if (panel != null)
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No panel has been assigned!");
        }

        // Start the coroutine to wait and then change the scene
        StartCoroutine(ChangeSceneAfterDelay());
    }

    // Coroutine to handle the delay and scene change
    private IEnumerator ChangeSceneAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the target scene
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogWarning("No scene has been assigned!");
        }
    }
}