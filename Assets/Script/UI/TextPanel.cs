using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextPanel : MonoBehaviour
{
    // SerializeFields for Panels and Texts
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private List<Text> texts;

    private Stack<int> storyProgressStack = new Stack<int>();
    private int currentIndex = -1; // Start before the first index

    // Start is called before the first frame update
    void Start()
    {
        // Initialize all panels and texts to inactive
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        foreach (Text text in texts)
        {
            text.gameObject.SetActive(false);
        }

        // Push the initial state into the stack
        storyProgressStack.Push(-1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextStory();
        }
    }

    void ShowNextStory()
    {
        // Hide the current panel and text if any are showing
        if (currentIndex >= 0 && currentIndex < panels.Count)
        {
            panels[currentIndex].SetActive(false);
            texts[currentIndex].gameObject.SetActive(false);
        }

        // Move to the next index
        currentIndex = storyProgressStack.Pop() + 1;

        // Check if there's a next panel and text to show
        if (currentIndex < panels.Count && currentIndex < texts.Count)
        {
            panels[currentIndex].SetActive(true);
            texts[currentIndex].gameObject.SetActive(true);
            storyProgressStack.Push(currentIndex); // Push current progress back to the stack
        }
    }
}