using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    public bool isOccupied = false;
    public GameObject placedItem = null; // Reference to the placed draggable item
    public int dataSetIndex = -1; // Assign in Inspector: 0 for Name, 1 for NIK, etc.

    public void PlaceItem(GameObject item)
    {
        placedItem = item;
        isOccupied = true;
    }

    public void Clear()
    {
        placedItem = null;
        isOccupied = false;

        // Optionally, destroy the placed item if needed
        // Destroy(placedItem);
    }
}
