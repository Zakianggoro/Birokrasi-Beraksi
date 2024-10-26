using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private int winConditionPoints;
    private int points = 0;

    public void AccumulatedPoints(int point)
    {
        points += point;
    }
    public void NextLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Debug.Log($"Moving to next level with {points} points");
        // Add logic to transition to the next level here
    }

}
