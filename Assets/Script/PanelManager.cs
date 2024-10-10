using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    // Reference to the panel GameObjects
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;
    public GameObject Panel5;

    // Duration of the scaling animation
    public float animationDuration = 0.3f;

    // Method to toggle Panel1's visibility with animation
    public void OpenPanel1()
    {
        if (Panel1 != null)
        {
            Debug.Log("Panel1 Active: " + Panel1.activeSelf);

            if (Panel1.activeSelf)
            {
                Debug.Log("Deactivating Panel1");
                StartCoroutine(ScalePanel(Panel1, Vector3.one, Vector3.zero, animationDuration, false)); // Scale down
            }
            else
            {
                Debug.Log("Activating Panel1");
                Panel1.SetActive(true);
                StartCoroutine(ScalePanel(Panel1, Vector3.zero, Vector3.one, animationDuration, true)); // Scale up
            }
        }
    }

    // Method to toggle Panel2's visibility with animation
    public void OpenPanel2()
    {
        if (Panel2 != null)
        {
            Debug.Log("Panel2 Active: " + Panel2.activeSelf);

            if (Panel2.activeSelf)
            {
                Debug.Log("Deactivating Panel2");
                StartCoroutine(ScalePanel(Panel2, Vector3.one, Vector3.zero, animationDuration, false)); // Scale down
            }
            else
            {
                Debug.Log("Activating Panel2");
                Panel2.SetActive(true);
                StartCoroutine(ScalePanel(Panel2, Vector3.zero, Vector3.one, animationDuration, true)); // Scale up
            }
        }
    }

    // Same approach for Panel3, Panel4, Panel5
    public void OpenPanel3()
    {
        if (Panel3 != null)
        {
            if (Panel3.activeSelf)
            {
                StartCoroutine(ScalePanel(Panel3, Vector3.one, Vector3.zero, animationDuration, false));
            }
            else
            {
                Panel3.SetActive(true);
                StartCoroutine(ScalePanel(Panel3, Vector3.zero, Vector3.one, animationDuration, true));
            }
        }
    }

    public void OpenPanel4()
    {
        if (Panel4 != null)
        {
            if (Panel4.activeSelf)
            {
                StartCoroutine(ScalePanel(Panel4, Vector3.one, Vector3.zero, animationDuration, false));
            }
            else
            {
                Panel4.SetActive(true);
                StartCoroutine(ScalePanel(Panel4, Vector3.zero, Vector3.one, animationDuration, true));
            }
        }
    }

    public void OpenPanel5()
    {
        if (Panel5 != null)
        {
            if (Panel5.activeSelf)
            {
                StartCoroutine(ScalePanel(Panel5, Vector3.one, Vector3.zero, animationDuration, false));
            }
            else
            {
                Panel5.SetActive(true);
                StartCoroutine(ScalePanel(Panel5, Vector3.zero, Vector3.one, animationDuration, true));
            }
        }
    }

    // Coroutine to scale the panel
    private IEnumerator ScalePanel(GameObject panel, Vector3 startScale, Vector3 endScale, float duration, bool activateOnEnd)
    {
        RectTransform panelRectTransform = panel.GetComponent<RectTransform>();

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            panelRectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelRectTransform.localScale = endScale;

        if (!activateOnEnd)
        {
            panel.SetActive(false);
        }
    }
}