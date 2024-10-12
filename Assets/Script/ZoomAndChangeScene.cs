using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoomAndChangeScene : MonoBehaviour
{
    public RectTransform monitorPanel; // Reference to the monitor's RectTransform
    public int sceneIndex; // Index of the next scene to load
    public float zoomDuration = 1f; // Time for the zoom-out animation
    public Vector3 zoomTargetScale = new Vector3(3f, 3f, 3f); // Target scale for zoom-out (3x bigger)

    [SerializeField] private GameObject[] panelsToDeactivate; // Panels to deactivate before zooming
    [SerializeField] private float deactivateDelay = 2f; // Delay before zooming out after deactivating panels

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
        // Deactivate specified panels with animation
        foreach (GameObject panel in panelsToDeactivate)
        {
            // Start deactivation animation
            yield return StartCoroutine(AnimatePanelOut(panel));
        }

        // Wait for the specified delay before starting the zoom-out animation
        yield return new WaitForSeconds(deactivateDelay);

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

    private IEnumerator AnimatePanelOut(GameObject panel)
    {
        RectTransform panelRectTransform = panel.GetComponent<RectTransform>();
        Vector3 initialScale = panelRectTransform.localScale;

        // Animate the panel out (scale down)
        float animationDuration = 0.5f; // Duration for the panel animation
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            // Scale down the panel
            panelRectTransform.localScale = Vector3.Lerp(initialScale, Vector3.zero, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the panel is completely hidden
        panelRectTransform.localScale = Vector3.zero;
        panel.SetActive(false); // Deactivate the panel
    }
}