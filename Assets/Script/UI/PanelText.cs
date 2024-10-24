using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;

public class PanelText : MonoBehaviour
{
    public List<string> textList;  // List of texts to be displayed
    public TextMeshProUGUI tmpText;  // Reference to the TextMeshPro component
    private int currentTextIndex = 0;  // Index of the current text being displayed

    void Start()
    {
        if (tmpText == null)
        {
            Debug.LogError("TextMeshPro component is not assigned!");
            return;
        }

        if (textList.Count > 0)
        {
            DisplayText();
        }
        else
        {
            Debug.LogError("No text in the text list!");
        }

        // Add a listener to the panel's button or UI component
        Button panelButton = GetComponent<Button>();
        if (panelButton != null)
        {
            panelButton.onClick.AddListener(OnPanelClick);
        }
        else
        {
            Debug.LogError("Button component is missing!");
        }
    }

    // Function to display the current text in the TMP component
    void DisplayText()
    {
        tmpText.text = textList[currentTextIndex];
    }

    // Called when the panel is clicked
    void OnPanelClick()
    {
        if (currentTextIndex < textList.Count - 1)
        {
            currentTextIndex++;
            DisplayText();
        }
        else
        {
            // If it's the last text, deactivate the panel
            gameObject.SetActive(false);
        }
    }
}