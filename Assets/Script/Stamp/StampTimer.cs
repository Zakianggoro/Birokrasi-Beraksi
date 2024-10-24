using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StampTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;

    private bool timerActive = false;

    public void StartTimer()
    {
        timerActive = true;
        StartCoroutine(TimerCountdown());
    }

    private IEnumerator TimerCountdown()
    {
        while (remainingTime > 0 && timerActive)
        {
            yield return new WaitForSeconds(1f);
            remainingTime -= 1;
            UpdateTimerText();
        }

        if (remainingTime <= 0)
        {
            StampManager.instance.TimeUp();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
