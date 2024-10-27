using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private string levelName;
    private float winConditionPoints = 0.8f;
    private int formPoints = 0;
    private int totalForm = 14;
    private int tfPoints = 0;
    private int totalTF = 14;
    private int stampPoints = 0;
    private int totalStamp = 5;

    private int points = 0;
    private string finalResult;

    public void AccumulatedPoints(int point)
    {
        points += point;
        Debug.Log($"You have {points} points");
    }

    public void AccumulatedPointsForm(int point)
    {
        formPoints += point;
        Debug.Log($"You have {formPoints} Form Points");
    }

    public void AccumulatedPointsTF(int point)
    {
        tfPoints += point;
        Debug.Log($"You have {tfPoints} T-F Points");
    }

    public void AccumulatedPointsStamp(int point)
    {
        stampPoints += point;
        Debug.Log($"You have {stampPoints} Stamp Points");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(levelName);
        Debug.Log($"Moving to next level with {points} points");
        // Add logic to transition to the next level here
    }

    public string getResult()
    {
        return finalResult;
    }

    public void FinalCheck()
    {
        float finalScore = (float) (((formPoints/totalForm)*0.5)+((tfPoints/totalTF)*0.3)+((stampPoints/totalStamp)*0.2));

        if (finalScore >= winConditionPoints)
        {
            //Win
            finalResult = "Win"; 
        }
        else if(finalScore < winConditionPoints)
        {
            //Lose
            finalResult = "Lose";
        }
        else
        {
            // ???
            Debug.Log("Invalid Result");
        }
    }
}
