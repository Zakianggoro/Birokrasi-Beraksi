using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [SerializeField] private SpriteStackManager spriteStackManager;
    [SerializeField] private ScoreManager scoreManager;
    public int boxType = -1;

    public void CheckScoreOnRelease(DragSprite sprite)
    {
        // Compare the sprite's type with the boxType
        if (sprite.typeNo == boxType)
        {
            scoreManager.AddScore();
            Debug.Log("Score increased by 1!");
        }

        Debug.Log("Sprite released in the trigger area and will be deactivated!");

        // Call the method to deactivate the current sprite and activate the next one
        spriteStackManager.DeactivateAndActivateNext();
    }
}
