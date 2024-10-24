using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Komik : MonoBehaviour
{
    [SerializeField] private Animator comicAnimator; // Single animator handling the entire sequence
    [SerializeField] private string animationTrigger = "StartComic"; // Trigger to start the comic animation

    private int pauseCount = 0; // Tracks how many pauses have happened
    private bool canProceed = false;  // Controls whether the player can click to continue

    void Start()
    {
        // Start the comic animation
        comicAnimator.SetTrigger(animationTrigger);
        comicAnimator.speed = 0.5f;
        canProceed = false;
    }

    void Update()
    {
        // Wait for player input to proceed the animation after a pause
        if (canProceed && Input.GetMouseButtonDown(0))
        {
            ResumeAnimation();
        }
    }

    // Animation event: Call this at key moments in the animation when panels slide into place
    public void PauseForPlayerInput()
    {
        canProceed = true;
        comicAnimator.speed = 0;  // Pause the animation
        Debug.Log($"Paused at panel {pauseCount + 1}, waiting for player input.");
    }

    void ResumeAnimation()
    {
        canProceed = false;
        pauseCount++;
        comicAnimator.speed = 0.25f;  // Resume the animation
        Debug.Log($"Resuming animation from panel {pauseCount}.");

        if( pauseCount == 4 )
        {
            OnComicEnd();
        }
    }

    // Animation event: Call this when the full animation is done
    public void OnComicEnd()
    {
        Debug.Log("Comic animation sequence complete.");
        // Optionally, trigger another action, such as progressing to the next level
        SceneManager.LoadScene("Main Menu");
    }
}
