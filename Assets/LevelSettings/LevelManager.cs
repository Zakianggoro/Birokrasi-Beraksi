using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private string levelName;
    private int winConditionPoints;
    private int points = 0;

    public void AccumulatedPoints(int point)
    {
        points += point;
        Debug.Log($"You have {points} points");
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(levelName);
        Debug.Log($"Moving to next level with {points} points");
        // Add logic to transition to the next level here
    }

}
