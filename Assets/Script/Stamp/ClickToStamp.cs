using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickToStamp : MonoBehaviour
{
    public TextMeshProUGUI totalClicksText;
    [SerializeField] private float goalClick = 60;
    private float totalClicks;

    private void Update()
    {
        
    }

    public void AddClicks()
    {
        totalClicks++;
        totalClicksText.text = totalClicks.ToString() + "/" + goalClick.ToString();
    }

    public void TotalPoints(int points)
    {

    }
}
