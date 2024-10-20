using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int winConditionPoints;
    private int points;

    public void NextLevel(int correctDocuments)
    {
        Debug.Log($"Moving to next level with {correctDocuments} correct documents.");
        // Add logic to transition to the next level here
    }

}
