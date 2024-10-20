using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance to access ScoreManager

    // Public or serialized TMP_Text field to reference the UI TextMeshPro component
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private LevelManager levelManager;

    private int score = 0;  // Score counter

    private void Awake()
    {
        // Singleton pattern setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);  // Ensure only one ScoreManager instance exists
        }
    }

    // Method to add points to the score
    public void AddScore()
    {
        score++;
        UpdateScoreText();  // Update the UI text
    }

    public void FinalScore()
    {
        levelManager.AccumulatedPoints(score);
    }

    // Method to update the score text UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("Score Text component is not assigned in the Inspector.");
        }
    }
}