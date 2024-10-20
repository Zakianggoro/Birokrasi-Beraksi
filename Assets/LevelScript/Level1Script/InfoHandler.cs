using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private DropArea[] dropBoxes; // Array of DropBoxes (DropAreas)

    private Vector3[] originalPositions; // To store original positions of draggable items
    private int currentPersonIndex = 0;  // Current person index in the PersonalData list
    private int currentDataSetIndex = 0; // To track which part of data we're on (name, NIK, etc.)
    private int correctDocuments = 0; // Track how many documents are correct
    private string[] playerInput = new string[5]; // Store the player's input (one for each field)
    private List<GameObject> duplicates = new List<GameObject>();

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

        currentDataSetIndex++;

        // Reset draggable items for the next data set
        ResetDraggableItemsPosition();

        // Ensure drop areas are cleared before assigning the next data set
        ResetAllDropAreas();

        // After the player completes all fields, check if the data is correct
        if (currentDataSetIndex >= 5) // Assuming 5 fields
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
            if (playerInput[0] == correctData.bioNIK) matches++;
            if (playerInput[1] == correctData.bioName) matches++;
            if (playerInput[2] == correctData.bioTanggalLahir) matches++;
            if (playerInput[3] == correctData.bioAlamat) matches++;
            if (playerInput[4] == correctData.bioRequest) matches++;

            Debug.Log($"Player filled {matches} fields correctly out of 5.");
            correctDocuments += matches;

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
            FindObjectOfType<LevelManager>().NextLevel(correctDocuments);
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
            default:
                Debug.LogWarning("No more data sets to assign.");
                break;
        }
    }
}
