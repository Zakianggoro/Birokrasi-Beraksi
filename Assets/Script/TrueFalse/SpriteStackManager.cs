using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteStackManager : MonoBehaviour
{
    // A list to hold the sprites in the stack, serialized for the Inspector
    [SerializeField] private List<GameObject> spriteList = new List<GameObject>();

    private Queue<GameObject> spriteQueue;  // Queue to manage the sprites
    private GameObject currentSprite;  // The currently active sprite

    private void Start()
    {
        // Initialize the queue from the list
        spriteQueue = new Queue<GameObject>(spriteList);

        // Activate the first sprite if the queue has elements
        ActivateNextSprite();
    }

    // Method to deactivate all sprites in the list
    private void DeactivateAllSprites()
    {
        foreach (var sprite in spriteList)
        {
            sprite.SetActive(false);
        }
    }

    // Method to activate the next sprite in the queue
    private void ActivateNextSprite()
    {
        // Deactivate all sprites before activating the next one
        DeactivateAllSprites();

        if (spriteQueue.Count > 0)
        {
            currentSprite = spriteQueue.Dequeue();  // Get the next sprite from the queue
            currentSprite.SetActive(true);  // Activate the sprite
            Debug.Log("Activated sprite: " + currentSprite.name);  // Log activation
        }
        else
        {
            Debug.Log("All sprites have been activated!");
        }
    }

    // Method to deactivate the current sprite and activate the next one
    public void DeactivateAndActivateNext()
    {
        if (currentSprite != null)
        {
            // Deactivate the current sprite
            Debug.Log("Deactivated sprite: " + currentSprite.name);
            currentSprite.SetActive(false);

            // Activate the next sprite if there are more in the queue
            ActivateNextSprite();
        }
    }
}