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

    private void Start()
    {
        // Ensure the panel is hidden at the start
        documentPanel.SetActive(false);
    }

    // This method gets called when clicking the folder GameObject
    private void OnMouseDown()
    {
        Debug.Log("Folder clicked!"); // Check if this is triggered

        if (!isPanelOpen)
        {
            ShowDocumentPanel();
        }
    }

    private void Update()
    {
        // Close the panel if it is open and the user clicks outside of it
        if (isPanelOpen && Input.GetMouseButtonDown(0) && !IsMouseOverPanel())
        {
            Debug.Log("Clicked outside panel, closing.");
            CloseDocumentPanel();
        }
    }

    private void ShowDocumentPanel()
    {
        Debug.Log("Showing document panel...");

        // Fill in the KTP and Kronologi details
        textKTP.text = GetPersonDetails();
        textKronologi.text = kronologi;

        // Show the panel
        documentPanel.SetActive(true);
        isPanelOpen = true;
    }

    private void CloseDocumentPanel()
    {
        Debug.Log("Closing document panel...");

        // Hide the panel
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
        // Use RectTransformUtility to check if the mouse is over the panel UI
        RectTransform panelRectTransform = documentPanel.GetComponent<RectTransform>();
        Vector2 localMousePosition = panelRectTransform.InverseTransformPoint(Input.mousePosition);
        return panelRectTransform.rect.Contains(localMousePosition);
    }
}
