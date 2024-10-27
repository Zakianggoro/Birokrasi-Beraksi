using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickToStamp : MonoBehaviour
{
    public TextMeshProUGUI totalClicksText;
    public string result;
    [SerializeField] private float goalClick = 60f;  // Assuming this is a float for goal comparison
    [SerializeField] private Animator anim;
    [SerializeField] private Animator finishAnim;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private LevelManager levelManager;

    [SerializeField] private StampTimer stampTimer;  // Reference to StampTimer

    private float totalClicks = 0;  // Total clicks as a float to handle progress accurately

    private void Start()
    {
        winPanel.SetActive(false);
    }

    public void AddClicks()
    {
        totalClicks++;
        anim.SetTrigger("Stamp");  // Trigger the stamp animation on each click

        result = totalClicks.ToString() + "/" + goalClick.ToString();
        totalClicksText.text = result;

        if (totalClicks >= goalClick)
        {
            // Stop the timer when the goal is reached
            stampTimer.StopTimer();
            winPanel.SetActive(true);
            GoalReached();
            Debug.Log("Goal Reached");
        }
    }

    public void GoalReached()
    {
        finishAnim.SetTrigger("StampComplete");
        FinalScore();
    }

    public void FinalScore()
    {
        int score = 1;
        levelManager.AccumulatedPointsStamp(score);
        Debug.Log("Stamp Score: +" + score);
    }
}
