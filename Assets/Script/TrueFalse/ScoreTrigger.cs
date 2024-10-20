using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [SerializeField] private SpriteStackManager spriteStackManager;
    [SerializeField] private  ScoreManager scoreManager;
    public int boxType = -1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the current sprite
        if (other.gameObject.CompareTag("ActiveSprite"))  // Ensure the sprite has this tag
        {
            if (other.GetComponent<DragSprite>().typeNo == boxType)
            {
                scoreManager.AddScore();
                Debug.Log("Score increased by 1!");
            }
            Debug.Log("Sprite entered the trigger area and will be deactivated!");
            
            // Call the method to deactivate the current sprite and activate the next one
            spriteStackManager.DeactivateAndActivateNext();
        }
    }
}