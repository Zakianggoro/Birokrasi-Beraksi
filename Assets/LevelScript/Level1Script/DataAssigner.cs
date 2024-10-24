using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataAssigner : MonoBehaviour
{
    [SerializeField] private PersonalData personalData; // Reference to the ScriptableObject
    [SerializeField] private TextMeshProUGUI nameText;  // The UI text element for the name
    [SerializeField] private TextMeshProUGUI nikText;   // The UI text element for NIK
    [SerializeField] private TextMeshProUGUI tanggalLahirText; // For birth date
    [SerializeField] private TextMeshProUGUI alamatText;  // For address
    [SerializeField] private TextMeshProUGUI requestText; // For request

    // Optional: References to field backgrounds for highlighting
    [SerializeField] private Image[] fieldBackgrounds; // Assign in Inspector

    public int currentIndex = 0; // Current index in the personal data list

    private void Start()
    {
        // Log the start event for debugging purposes
        Debug.Log("DataAssigner Start - Checking Personal Data");

        if (personalData != null && personalData.personalEntries.Count > 0)
        {
            Debug.Log("Personal Data Found: " + personalData.personalEntries.Count + " entries available.");
            DisplayPersonalData(currentIndex); // Display the first entry at start
        }
        else
        {
            Debug.LogWarning("Personal Data is empty or not assigned.");
        }
    }

    public PersonalData PersonalData
    {
        get { return personalData; }
        set { personalData = value; }
    }

    // Assigns data to UI text elements
    public void DisplayPersonalData(int index)
    {
        // Check if the index is valid
        if (personalData == null)
        {
            Debug.LogError("Personal Data is not assigned!");
            return;
        }

        if (index >= 0 && index < personalData.personalEntries.Count)
        {
            // Get the current entry and assign it to the UI
            PersonalEntry entry = personalData.personalEntries[index];
            nameText.text = entry.bioName;
            nikText.text = entry.bioNIK;
            tanggalLahirText.text = entry.bioTanggalLahir;
            alamatText.text = entry.bioAlamat;
            requestText.text = entry.bioPurpose;

            // Log the successful data assignment for debugging
            Debug.Log("Displaying Personal Data for index: " + index);
            Debug.Log($"Name: {entry.bioName}, NIK: {entry.bioNIK}, Date of Birth: {entry.bioTanggalLahir}, Address: {entry.bioAlamat}, Request: {entry.bioPurpose}");
        }
        else
        {
            Debug.LogWarning("Index out of bounds: No more entries to display.");
        }
    }

    // Optional: Expose field backgrounds for highlighting
    public Image[] FieldBackgrounds => fieldBackgrounds;
}
