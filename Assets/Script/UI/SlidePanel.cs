using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidePanel : MonoBehaviour
{
    public RectTransform panel;  // Assign the panel in the Inspector
    public Button moveButton;    // Assign the button in the Inspector
    public float moveDuration = 1.0f;  // Duration of the movement

    private Vector2 startPos = new Vector2(-744.9972f, -697f);
    private Vector2 endPos = new Vector2(-744.9972f, -169.3f);
    private bool isMoving = false;
    private float elapsedTime = 0;

    void Start()
    {
        // Set the panel to the start position
        panel.anchoredPosition = startPos;

        // Add listener to button click
        moveButton.onClick.AddListener(StartPanelMovement);
    }

    void Update()
    {
        if (isMoving)
        {
            // Move the panel smoothly over the duration
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            panel.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            // Stop moving when the panel reaches the destination
            if (t >= 1.0f)
            {
                isMoving = false;
                elapsedTime = 0;
            }
        }
    }

    void StartPanelMovement()
    {
        if (!isMoving)
        {
            isMoving = true;
        }
    }
}