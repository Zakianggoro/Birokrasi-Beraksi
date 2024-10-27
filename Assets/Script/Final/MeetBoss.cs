using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MeetBoss : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Animator anim;
    [SerializeField] private Image accept;
    [SerializeField] private Image decline;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private string acceptDialog;
    [SerializeField] private string declineDialog;

    private string result = "";
    private bool isAnimationComplete = false;

    private void Start()
    {

        levelManager.FinalCheck();

        Debug.Log("Meet Boss");
        resultPanel.SetActive(true);
        result = levelManager.getResult();

        if (result == "Win")
        {
            Debug.Log("Accepted");
            accept.enabled = true;
            decline.enabled = false;
            dialogText.text = acceptDialog;
        }
        else if (result == "Lose")
        {
            Debug.Log("Decline");
            decline.enabled = true;
            accept.enabled = false;
            dialogText.text = declineDialog;
        }
        else
        {
            Debug.Log("What?");
        }
        StartCoroutine(WaitForAnimation());
    }

    private void Update()
    {
        if (isAnimationComplete && Input.GetMouseButtonDown(0))
        {
            levelManager.NextLevel();
        }
    }

    private IEnumerator WaitForAnimation()
    {
        // Wait for the animation to complete
        yield return new WaitForSeconds(5);
        isAnimationComplete = true;
    }
}
