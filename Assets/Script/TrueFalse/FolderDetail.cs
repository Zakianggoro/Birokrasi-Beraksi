using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FolderDetail : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textKTP;       // Text for ID Card (KTP)
    [SerializeField] private TextMeshProUGUI textKronologi; // Text for Kronologi
    [SerializeField] private GameObject documentPanel;      // Panel to display details
    [SerializeField] private Names personInfo;              // Reference to the ScriptableObject holding names
    [SerializeField] private string kronologi;              // Kronologi text
    [SerializeField] private int personIndex;               // Index for the specific person's info
    private bool isPanelOpen = false;

    private DragSprite dragSprite;  // Reference to DragSprite

    // For detecting double-clicks
    private int clickCount = 0;                     // To track the number of clicks
    private float clickTimer = 0f;                  // To track the time between clicks
    private float doubleClickThreshold = 0.3f;      // Time window for detecting a double-click

    private void Start()
    {
        documentPanel.SetActive(false);
        dragSprite = GetComponent<DragSprite>();    // Assuming both scripts are on the same GameObject
    }

    private void Update()
    {
        // Count time between clicks
        if (clickCount == 1)
        {
            clickTimer += Time.deltaTime;
            if (clickTimer > doubleClickThreshold)
            {
                // Reset if no double-click occurs within the threshold
                clickCount = 0;
                clickTimer = 0f;
            }
        }

        // Close the panel if it's open and the user clicks outside of it
        if (isPanelOpen && Input.GetMouseButtonDown(0) && !IsMouseOverPanel())
        {
            CloseDocumentPanel();
        }
    }

    private void OnMouseDown()
    {
        if (dragSprite.IsDragging())
        {
            // Prevent showing the document while dragging
            return;
        }

        clickCount++;

        if (clickCount == 1)
        {
            // First click, start the timer
            clickTimer = 0f;
        }
        else if (clickCount == 2)
        {
            // Double-click detected
            clickCount = 0;
            ShowDocumentPanel();
        }
    }

    private void ShowDocumentPanel()
    {
        textKTP.text = GetPersonDetails();
        textKronologi.text = kronologi;
        documentPanel.SetActive(true);
        isPanelOpen = true;
    }

    private void CloseDocumentPanel()
    {
        documentPanel.SetActive(false);
        isPanelOpen = false;
    }

    private string GetPersonDetails()
    {
        if (personIndex < 0 || personIndex >= personInfo.names.Length)
        {
            return "Invalid Person Data";
        }

        string personDetails = $"Name       : {personInfo.names[personIndex]}\n" +
                               $"NIK        : {personInfo.nik[personIndex]}\n" +
                               $"DOB        : {personInfo.tanggalLahir[personIndex]}\n" +
                               $"Sex        : {personInfo.jenisKelamin[personIndex]}\n" +
                               $"Address    : {personInfo.alamat[personIndex]}\n" +
                               $"Status     : {personInfo.statusPerkawinan[personIndex]}\n" +
                               $"Pekerjaan  : {personInfo.pekerjaan[personIndex]}";
        return personDetails;
    }

    private bool IsMouseOverPanel()
    {
        RectTransform panelRectTransform = documentPanel.GetComponent<RectTransform>();
        Vector2 localMousePosition = panelRectTransform.InverseTransformPoint(Input.mousePosition);
        return panelRectTransform.rect.Contains(localMousePosition);
    }
}
