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
    public Button clickButton;
    public GameObject losePanel;
    public GameObject winPanel;

    [SerializeField] private StampTimer timer;
    [SerializeField] private ClickToStamp clickCounter;
    [SerializeField] private Animator anim;
    [SerializeField] private ClickToStamp clickToStamp;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private string levelName;
    private bool gameStarted = false;
    private bool gameFinished = false;
    private bool canProceed = false;

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
        losePanel.SetActive(false);
        StartPanel.SetActive(true);

        //resultPanel.SetActive(false);

        /*retryButton.gameObject.SetActive(false);
        retryButton.onClick.AddListener(ReloadGame);*/
    }

    private void Update()
    {
        if(gameFinished)
        {
            if (canProceed && Input.GetMouseButtonDown(0))
            {
                ResumeAnimation();
            }
        }
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


        clickButton.interactable = false;
        gameFinished = true;
        ShowLoseResult();
    }

    private void ShowLoseResult()
    {
        if (!winPanel.activeSelf)  // Checks if winPanel is NOT active
        {
            losePanel.SetActive(true);
            anim.SetTrigger("Lose");
            score.text = clickToStamp.result;
            clickToStamp.FinalScore();
        }
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

    public void PauseForPlayerInput()
    {
        canProceed = true;
        anim.speed = 0;  // Pause the animation
    }

    void ResumeAnimation()
    {
        canProceed = false;
        anim.speed = 0.25f;  // Resume the animation
    }

    public void OnAnimationEnd()
    {
        Debug.Log("Animation sequence complete.");
        SceneManager.LoadScene(levelName);
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
