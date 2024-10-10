using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoomAndChangeScene : MonoBehaviour
{
    public RectTransform monitorPanel; // Reference to the monitor's RectTransform
    public int sceneIndex; // Index of the next scene to load
    public float zoomDuration = 1f; // Time for the zoom-out animation
    public Vector3 zoomTargetScale = new Vector3(2f, 2f, 2f); // Target scale for zoom-out

    private bool isZoomingOut = false;
    private Vector3 initialScale;

    void Start()
    {
        // Store the initial scale of the monitor
        initialScale = monitorPanel.localScale;
    }

    public void ZoomOutAndChangeScene()
    {
        if (!isZoomingOut)
        {
            StartCoroutine(ZoomOut());
        }
    }

    private IEnumerator ZoomOut()
    {
        isZoomingOut = true;
        float elapsedTime = 0f;

        // Animate the scale over time
        while (elapsedTime < zoomDuration)
        {
            monitorPanel.localScale = Vector3.Lerp(initialScale, zoomTargetScale, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is set to the target
        monitorPanel.localScale = zoomTargetScale;

        // After zooming out, load the next scene
        SceneManager.LoadScene(sceneIndex);
    }
}