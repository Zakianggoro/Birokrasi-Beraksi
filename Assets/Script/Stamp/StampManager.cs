using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StampManager : MonoBehaviour
{
    public static StampManager instance;

    public GameObject timerObject;
    public GameObject clickCounterObject;
    public GameObject StartPanel;
    /*public TMP_Text resultText;
    public GameObject resultPanel;
    public Image resultImage;
    public Sprite winImage;
    public Sprite loseImage;*/
    public Button clickButton;

    [SerializeField] private StampTimer timer;
    [SerializeField] private ClickToStamp clickCounter;

    private bool gameStarted = false;
    private bool gameFinished = false;

    // Objective completion event
    public delegate void ObjectiveCompleted();
    public static event ObjectiveCompleted OnObjectiveComplete;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timerObject.SetActive(false);
        clickCounterObject.SetActive(false);
        //resultPanel.SetActive(false);


        /*retryButton.gameObject.SetActive(false);
        retryButton.onClick.AddListener(ReloadGame);*/
    }

    public void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            StartPanel.SetActive(false);
            StartCoroutine(StartCountdownWithDelay(0f));

        }
    }

    private IEnumerator StartCountdownWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        timerObject.SetActive(true);
        clickCounterObject.SetActive(true);
        timer.StartTimer();
    }

    public void TimeUp()
    {
        if (gameFinished) return;

        gameFinished = true;
        clickButton.interactable = false;
        ShowLoseResult();
    }

    private void ShowLoseResult()
    {
/*        resultPanel.SetActive(true);
        resultText.text = "You Lose";
        resultImage.sprite = loseImage;
        retryButton.gameObject.SetActive(true);*/
    }

    private void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ObjectiveAchieved()
    {
        if (!gameFinished)
        {
            gameFinished = true;

            if (OnObjectiveComplete != null)
            {
                OnObjectiveComplete.Invoke();
            }

            Debug.Log("Objective Achieved - Event Triggered");
        }
    }



    public void ShowWinText()
    {
        /*resultText.text = "You Win";
        resultPanel.SetActive(true);*/
    }

    public void ShowWinImage()
    {
/*        resultImage.sprite = winImage;
        resultPanel.SetActive(true);*/
    }
}
