using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataAssigner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private PersonalData personalData; // Reference to the ScriptableObject
    [SerializeField] private DocumentType documentType;
    [SerializeField] private TextMeshProUGUI scrollText;

    [SerializeField] private TextMeshProUGUI nikText;   // The UI text element for NIK
    [SerializeField] private TextMeshProUGUI nameText;  // The UI text element for the name
    [SerializeField] private TextMeshProUGUI tanggalLahirText; // For birth date
    [SerializeField] private TextMeshProUGUI jenisKelaminText;
    [SerializeField] private TextMeshProUGUI alamatText;  // For address
    [SerializeField] private TextMeshProUGUI statusPerkawinanText; // For status
    [SerializeField] private TextMeshProUGUI pekerjaanText;   // The UI text element for NIK

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
            DisplayDataChronology(currentIndex);
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


            nikText.text = $"NIK : {entry.bioNIK}";
            nameText.text = entry.bioName;
            tanggalLahirText.text = entry.bioTanggalLahir;
            jenisKelaminText.text = entry.bioJenisKelamin;
            alamatText.text = entry.bioAlamat;
            statusPerkawinanText.text = entry.statusPerkawinan;
            pekerjaanText.text = entry.pekerjaan;

            // Log the successful data assignment for debugging
            Debug.Log("Displaying Personal Data for index: " + index);
            Debug.Log($"Name: {entry.bioName}, NIK: {entry.bioNIK}, Date of Birth: {entry.bioTanggalLahir}, Address: {entry.bioAlamat}, Request: {entry.bioPurpose}");
        }
        else
        {
            Debug.LogWarning("Index out of bounds: No more entries to display.");
        }
    }

    private string DisplayDataChronology(int index)
    {
        // Check if the index is valid
        if (personalData == null)
        {
            Debug.LogError("Personal Data is not assigned!");
            return string.Empty;  // Return an empty string instead of returning nothing
        }

        string content = "";

        if (index >= 0 && index < personalData.personalEntries.Count)
        {
            // Get the current entry
            PersonalEntry entry = personalData.personalEntries[index];

            // Build the string with the required data
            content = $"Nomor Surat  : {entry.bioNomorSurat}\n" +
                      $"Tujuan            : {entry.bioPurpose}\n" +
                      $"Kronologi        : \n" +
                      $"{entry.bioKronologi}";
        }
        else
        {
            Debug.LogWarning("Index out of bounds: No more entries to display.");
            content = "No data available";  // Return a message indicating no data
        }

        scrollText.text = content;
        return scrollText.text;  // Return the final content
    }


    // Optional: Expose field backgrounds for highlighting
    public Image[] FieldBackgrounds => fieldBackgrounds;
}
