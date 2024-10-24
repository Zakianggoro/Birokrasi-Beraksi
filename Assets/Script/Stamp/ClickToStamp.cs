using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickToStamp : MonoBehaviour
{
    public TextMeshProUGUI totalClicksText;
    [SerializeField] private float goalClick = 60;
    [SerializeField] private Animator anim;

    private float totalClicks;

    private void Update()
    {
        
    }

    public void AddClicks()
    {
        totalClicks++;
        anim.SetTrigger("Stamp");
        totalClicksText.text = totalClicks.ToString() + "/" + goalClick.ToString();
    }

    public void TotalPoints(int points)
    {

    }
}
