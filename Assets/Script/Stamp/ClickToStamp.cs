using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickToStamp : MonoBehaviour
{
    public TextMeshProUGUI totalClicksText;
    [SerializeField] private float goalClick = 60f;  // Assuming this is a float for goal comparison
    [SerializeField] private Animator anim;
    [SerializeField] private LevelManager levelManager;

    private float totalClicks = 0;  // Total clicks as a float to handle progress accurately

    private void Update()
    {
        // You might want to include game logic here for updates or key input
    }



    public void AddClicks()
    {
        totalClicks++;
        anim.SetTrigger("Stamp");  // Trigger the stamp animation on each click

        // Update the UI to show the current clicks and the goal
        totalClicksText.text = totalClicks.ToString() + "/" + goalClick.ToString();

        // Optionally, check if the goal is reached and trigger FinalScore if necessary
        if (totalClicks >= goalClick)
        {
            FinalScore();
        }
    }

    public void TotalPoints(int points)
    {
        // This method can be used to add or calculate additional points, if needed
        // Right now, it's a placeholder
    }

    public void FinalScore()
    {
        // Calculate score as a percentage of the total clicks compared to the goal
        float scorePercentage = totalClicks / goalClick;

        // Convert to an integer score, maybe multiplying by 100 for a percentage-like score
        int score = Mathf.RoundToInt(scorePercentage * 100);

        // Pass the score to the LevelManager
        levelManager.AccumulatedPoints(score);

        // Optionally, print or debug the final score
        Debug.Log("Final Score: " + score);
    }
}
