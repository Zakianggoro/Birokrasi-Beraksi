using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confirmation : MonoBehaviour
{
    public GameObject panel; // Assign your panel in the Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the panel's active state
            if (panel != null)
            {
                panel.SetActive(!panel.activeSelf);
            }
        }
    }
}
