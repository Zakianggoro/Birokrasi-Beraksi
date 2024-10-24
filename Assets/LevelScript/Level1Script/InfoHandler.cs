using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoHandler : MonoBehaviour
{
    [SerializeField] private Names data; // ScriptableObject holding the arrays
    [SerializeField] private GameObject[] draggableItems; // Reference to the draggable elements
    [SerializeField] private TextMeshPro[] draggableTexts; // The text elements inside each draggable item
    [SerializeField] private Button confirmButton;
    [SerializeField] private DataAssigner dataAssigner; // Reference to assign data to the form UI
    [SerializeField] private DocumentType documentType;

   [SerializeField] private DropArea[] dropBoxes; // Array of DropBoxes (DropAreas)

    private Vector3[] originalPositions; // To store original positions of draggable items
    private int currentPersonIndex = 0;  // Current person index in the PersonalData list
    private int currentDataSetIndex = 0; // To track which part of data we're on (name, NIK, etc.)
    private int correctDocuments = 0; // Track how many documents are correct
    private string[] playerInput = new string[16]; // Store the player's input (one for each field)
    private List<GameObject> duplicates = new List<GameObject>();

    public LevelManager levelManager;

    private void Start()
    {
        originalPositions = new Vector3[draggableItems.Length];
        for (int i = 0; i < draggableItems.Length; i++)
        {
            originalPositions[i] = draggableItems[i].transform.position;
        }

        AssignDataToDraggableItems(data.names);
        dataAssigner.DisplayPersonalData(currentPersonIndex);
        confirmButton.onClick.AddListener(ConfirmSelection);
    }

    private void ConfirmSelection()
    {
        Debug.Log("ConfirmSelection called");

        // Iterate through each DropArea to process placed items
        foreach (DropArea dropArea in dropBoxes)
        {
            if (dropArea.isOccupied && dropArea.placedItem != null)
            {
                GameObject draggedItem = dropArea.placedItem;
                DragDrop dragDrop = draggedItem.GetComponent<DragDrop>();

                if (dragDrop != null && !dragDrop.isConfirmed)
                {
                    Debug.Log($"Processing DropArea with dataSetIndex {dropArea.dataSetIndex} and item {draggedItem.name}");

                    // Mark as confirmed to prevent re-processing
                    dragDrop.isConfirmed = true;

                    // Save the player's input based on dataSetIndex
                    int dataSetIndex = dropArea.dataSetIndex;
                    if (dataSetIndex >= 0 && dataSetIndex < playerInput.Length)
                    {
                        playerInput[dataSetIndex] = GetItemText(draggedItem);
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid dataSetIndex {dataSetIndex} for DropArea {dropArea.name}");
                    }

                    // Duplicate and disable the dragged item within the drop area
                    DuplicateAndDisable(draggedItem, dropArea);
                }
            }
        }

        Debug.Log($"Current document type before check: {documentType.document}");

        if (documentType.document == "")
        {
            Debug.Log("Document is Null");
            return;
        }
        else
        {
            currentDataSetIndex++;
            Debug.Log("Data Index +1");
        }


        // Reset draggable items for the next data set
        ResetDraggableItemsPosition();

        // Ensure drop areas are cleared before assigning the next data set
        ResetAllDropAreas();

        switch (documentType.document)
        {
            case "Kehilangan":
                // After the player completes all fields, check if the data is correct
                if (currentDataSetIndex >= 14) // Assuming 5 fields
                {
                    CheckDataCorrectness();
                    currentDataSetIndex = 0;
                    NextPerson(); // Move to the next person
                }
                else
                {
                    // Assign next dataset (nik, tanggalLahir, alamat, request)
                    AssignNextDataSet();
                }
                break;

            case "Domisili":
                // Check the relevant fields for "Domisili"
                // After the player completes all fields, check if the data is correct
                if (currentDataSetIndex >= 10) // Assuming 5 fields
                {
                    CheckDataCorrectness();
                    currentDataSetIndex = 0;
                    NextPerson(); // Move to the next person
                }
                else
                {
                    // Assign next dataset (nik, tanggalLahir, alamat, request)
                    AssignNextDataSet();
                }
                Debug.Log("Domisili");
                break;

            case "Usaha":
                // Check the relevant fields for "Usaha"
                // After the player completes all fields, check if the data is correct
                if (currentDataSetIndex >= 12) // Assuming 5 fields
                {
                    CheckDataCorrectness();
                    currentDataSetIndex = 0;
                    NextPerson(); // Move to the next person
                }
                else
                {
                    // Assign next dataset (nik, tanggalLahir, alamat, request)
                    AssignNextDataSet();
                }
                break;

            default:
                Debug.LogWarning("No valid document type selected.");
                break;
        }
    }

    private string GetItemText(GameObject draggedItem)
    {
        TextMeshPro tmp = draggedItem.GetComponentInChildren<TextMeshPro>();
        return tmp != null ? tmp.text : "";
    }

    public void DuplicateAndDisable(GameObject draggedItem, DropArea dropArea)
    {
        GameObject duplicate = Instantiate(draggedItem);
        duplicate.transform.position = dropArea.transform.position; // Align to drop area, not dragged item position
        duplicate.transform.rotation = draggedItem.transform.rotation;
        duplicate.transform.localScale = draggedItem.transform.localScale;
        duplicate.transform.SetParent(dropArea.transform, true);

        var collider = duplicate.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        var dragHandler = duplicate.GetComponent<DragDrop>();
        if (dragHandler != null) dragHandler.enabled = false;

        draggedItem.SetActive(false); // Hide the original dragged item

        duplicates.Add(duplicate); // Keep track of duplicates
    }


    public void ClearDuplicates()
    {
        foreach (var duplicate in duplicates)
        {
            Destroy(duplicate);
        }
        duplicates.Clear();
    }

    private void AssignDataToDraggableItems(string[] dataSet)
    {
        for (int i = 0; i < draggableItems.Length; i++)
        {
            if (i < dataSet.Length)
            {
                draggableTexts[i].text = dataSet[i];
                draggableItems[i].SetActive(true);

                // Reset confirmation flag
                DragDrop dragDrop = draggableItems[i].GetComponent<DragDrop>();
                if (dragDrop != null)
                {
                    dragDrop.isConfirmed = false;
                }
            }
            else
            {
                draggableItems[i].SetActive(false);
            }
        }
    }

    private void CheckDataCorrectness()
    {
        if (currentPersonIndex >= 0 && currentPersonIndex < dataAssigner.PersonalData.personalEntries.Count)
        {
            PersonalEntry correctData = dataAssigner.PersonalData.personalEntries[currentPersonIndex];

            int matches = 0;

            if (!string.IsNullOrEmpty(playerInput[0]) && playerInput[0] == correctData.bioNomorSurat) matches++;
            if (!string.IsNullOrEmpty(playerInput[1]) && playerInput[1] == correctData.bioNIK) matches++;
            if (!string.IsNullOrEmpty(playerInput[2]) && playerInput[2] == correctData.bioName) matches++;
            if (!string.IsNullOrEmpty(playerInput[3]) && playerInput[3] == correctData.bioTanggalLahir) matches++;
            if (!string.IsNullOrEmpty(playerInput[4]) && playerInput[4] == correctData.bioJenisKelamin) matches++;
            if (!string.IsNullOrEmpty(playerInput[5]) && playerInput[5] == correctData.statusPerkawinan) matches++;
            if (!string.IsNullOrEmpty(playerInput[6]) && playerInput[6] == correctData.pekerjaan) matches++;
            if (!string.IsNullOrEmpty(playerInput[7]) && playerInput[7] == correctData.bioAlamat) matches++;
            if (!string.IsNullOrEmpty(playerInput[8]) && playerInput[8] == correctData.bioPurpose) matches++;
            if (!string.IsNullOrEmpty(playerInput[9]) && playerInput[9] == correctData.bioNamaPemohon) matches++;

            // Use the document type to modify how the data is checked
            switch (documentType.document)
            {
                case "Kehilangan":
                    if (!string.IsNullOrEmpty(playerInput[10]) && playerInput[10] == correctData.bioTanggalSurat) matches++;
                    if (!string.IsNullOrEmpty(playerInput[11]) && playerInput[11] == correctData.bioBarang) matches++;
                    if (!string.IsNullOrEmpty(playerInput[12]) && playerInput[12] == correctData.bioAtasNama) matches++;
                    if (!string.IsNullOrEmpty(playerInput[13]) && playerInput[13] == correctData.bioAtasNIK) matches++;
                    break;

                case "Domisili":
                    // Check the relevant fields for "Domisili"
                    Debug.Log("Domisili");
                    break;

                case "Usaha":
                    // Check the relevant fields for "Usaha"
                    if (!string.IsNullOrEmpty(playerInput[14]) && playerInput[14] == correctData.bioJenisJasa) matches++;
                    if (!string.IsNullOrEmpty(playerInput[15]) && playerInput[15] == correctData.bioLetakJasa) matches++;
                    break;

                default:
                    Debug.LogWarning("No valid document type selected.");
                    break;
            }

            Debug.Log($"Player filled {matches} fields correctly out of 5.");

            switch (documentType.document)
            {
                case "Kehilangan":
                    if (matches == 14)
                    {
                        correctDocuments++;
                    }
                    break;
                case "Domisili":
                    if (matches == 10)
                    {
                        correctDocuments++;
                    }
                    break;
                case "Usaha":
                    if (matches == 12)
                    {
                        correctDocuments++;
                    }
                    break;
                default:
                    Debug.Log("No valid document type selected");
                    break;
            }

            // Optionally, update UI to reflect the score
            UpdateScoreUI();
        }
        else
        {
            Debug.LogError("currentPersonIndex is out of range.");
        }
    }


    private void UpdateScoreUI()
    {
        // Implement your UI update logic here
        // Example:
        // scoreText.text = $"Score: {correctDocuments}";
    }

    private void NextPerson()
    {
        currentPersonIndex++;
        ResetAllDropAreas();

        if (currentPersonIndex < dataAssigner.PersonalData.personalEntries.Count)
        {
            dataAssigner.DisplayPersonalData(currentPersonIndex); // Show new person's data
            AssignDataToDraggableItems(data.names); // Restart with the 'name' dataset

            // Reset player input for the next person
            System.Array.Clear(playerInput, 0, playerInput.Length);
            ClearDuplicates();
        }
        else
        {
            Debug.Log($"All paperwork completed! {correctDocuments} correct fields in total.");
            FinalScore(correctDocuments);
            FindObjectOfType<LevelManager>().NextLevel("True-False");
        }
    }

    private void ResetAllDropAreas()
    {
        foreach (DropArea dropArea in dropBoxes)
        {
            dropArea.Clear(); // Reset drop areas for the next person
        }
    }


    private void ResetDraggableItemsPosition()
    {
        for (int i = 0; i < draggableItems.Length; i++)
        {
            // Reset the position to the original
            draggableItems[i].transform.position = originalPositions[i];

            // Ensure the draggable item is active again
            draggableItems[i].SetActive(true);

            // Reset the confirmation and drop area flags
            var dragDrop = draggableItems[i].GetComponent<DragDrop>();
            if (dragDrop != null)
            {
                dragDrop.isConfirmed = false;  // Reset the confirmation status
                dragDrop.currentDropArea = null; // Reset any references to the previous drop area
            }
        }
    }

    private void AssignNextDataSet()
    {
        switch (currentDataSetIndex)
        {
            case 1:
                AssignDataToDraggableItems(data.nik);
                break;
            case 2:
                AssignDataToDraggableItems(data.tanggalLahir);
                break;
            case 3:
                AssignDataToDraggableItems(data.alamat);
                break;
            case 4:
                AssignDataToDraggableItems(data.request);
                break;
            case 5:
                AssignDataToDraggableItems(data.noSurat);
                break;
            case 6:
                AssignDataToDraggableItems(data.jenisKelamin);
                break;
            case 7:
                AssignDataToDraggableItems(data.statusPerkawinan);
                break;
            case 8:
                AssignDataToDraggableItems(data.pekerjaan);
                break;
            case 9:
                AssignDataToDraggableItems(data.request);
                break;
            default:
                break;
        }

        Debug.Log("Document type: " + documentType.document);


        switch (documentType.document)
        {
            case "Kehilangan":
                switch (currentDataSetIndex)
                {
                    case 10:
                        AssignDataToDraggableItems(data.tanggalSurat);
                        break;
                    case 11:
                        AssignDataToDraggableItems(data.barang);
                        break;
                    case 12:
                        AssignDataToDraggableItems(data.atasNama);
                        break;
                    case 13:
                        AssignDataToDraggableItems(data.atasNIK);
                        break;
                    default:
                        break;
                }
                break;
            case "Domisili":
                break;
            case "Usaha":
                switch (currentDataSetIndex)
                {
                    case 10:
                        AssignDataToDraggableItems(data.jenisJasa);
                        break;
                    case 11:
                        AssignDataToDraggableItems(data.letakJasa);
                        break;
                    default:
                        Debug.LogWarning("No more data sets to assign.");
                        break;
                }
                break;
            default:
                Debug.Log("No valid document type selected");
                break;
        }

        Debug.Log("Current data set index: " + currentDataSetIndex);

    }

    private void FinalScore(int point)
    {
        levelManager.AccumulatedPoints(point);
    }
}
