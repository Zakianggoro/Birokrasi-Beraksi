using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Komik : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels; // List of panels containing the images
    [SerializeField] private List<Animator> animators; // List of animators for each panel's image

    private int currentPanelIndex = 0; // To keep track of the current panel being activated

    void Update()
    {
        // Check for any mouse click (left mouse button or touch)
        if (Input.GetMouseButtonDown(0))
        {
            ActivateNextImage();
        }
    }

    void ActivateNextImage()
    {
        // Check if there are panels left to activate
        if (currentPanelIndex < panels.Count)
        {
            // Activate the current panel and play its animation
            panels[currentPanelIndex].SetActive(true);
            Animator panelAnimator = animators[currentPanelIndex];
            panelAnimator.SetTrigger("ShowImage"); // Trigger animation (assuming you have a "ShowImage" trigger)

            currentPanelIndex++;
        }
        else
        {
            // If all panels have been activated, reset or do other actions
            Debug.Log("All images have been activated.");
        }
    }
}