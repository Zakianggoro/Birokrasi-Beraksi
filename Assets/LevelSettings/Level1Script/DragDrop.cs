using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    Vector3 offset;
    Vector3 originalPosition;
    Collider2D collider2d;
    public string destinationTag = "DropArea";
    private Collider2D lastDropAreaCollider;  // To keep track of the last drop area's collider
    public DropArea currentDropArea; // Reference to the current drop area
    public bool isConfirmed = false; // Flag to track if confirmed

    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        originalPosition = transform.position;  // Store the original position
        Debug.Log($"{gameObject.name} Awake called. Original position set to: {originalPosition}");
    }

    void OnMouseDown()
    {
        // Calculate offset in X and Y, discard Z
        offset = (Vector2)(transform.position - MouseWorldPosition());

        // Re-enable the collider of the last drop area when picking up the object again
        if (lastDropAreaCollider != null)
        {
            lastDropAreaCollider.enabled = true;
            Debug.Log($"Re-enabled collider for last drop area: {lastDropAreaCollider.name}");
        }

        // If the draggable item is currently in a drop area, free that drop area
        if (currentDropArea != null)
        {
            currentDropArea.isOccupied = false;
            currentDropArea.placedItem = null;
            currentDropArea = null;
            Debug.Log($"{gameObject.name} was picked up from a drop area. Drop area freed.");
        }

        // Re-enable collider on mouse down to allow dragging even when overlapping
        collider2d.enabled = true;
        Debug.Log($"{gameObject.name} Collider enabled for dragging.");
    }

    void OnMouseDrag()
    {
        // Apply offset during dragging, ignore Z-component
        transform.position = (Vector3)(MouseWorldPosition() + offset);

        // Change color of the drop area if the draggable object is hovering over it
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);

        if (hitCollider != null && hitCollider.CompareTag(destinationTag))
        {
            Debug.Log($"{gameObject.name} is hovering over drop area: {hitCollider.name}");
        }
        else if (lastDropAreaCollider != null)
        {
            Debug.Log($"{gameObject.name} is no longer hovering over drop area: {lastDropAreaCollider.name}");
        }
    }

    void OnMouseUp()
    {
        collider2d.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);

        if (hitCollider != null && hitCollider.CompareTag(destinationTag))
        {
            DropArea dropArea = hitCollider.GetComponent<DropArea>();
            if (dropArea != null && !dropArea.isOccupied)
            {
                // Snap the object to the drop area and mark the drop area as occupied
                transform.position = dropArea.transform.position;
                dropArea.PlaceItem(this.gameObject);
                currentDropArea = dropArea;
                lastDropAreaCollider = hitCollider;

                Debug.Log($"{gameObject.name} dropped into valid drop area: {hitCollider.name}. Position set to {transform.position}");
            }
            else
            {
                ReturnToOriginalPosition();
                Debug.Log($"{gameObject.name} drop area is already occupied or invalid. Returning to original position: {originalPosition}");
            }
        }
        else
        {
            ReturnToOriginalPosition();
            Debug.Log($"{gameObject.name} no valid drop area found. Returning to original position: {originalPosition}");
        }

        collider2d.enabled = true;
        Debug.Log($"{gameObject.name} OnMouseUp complete. Checking for drop area...");
    }

    void ReturnToOriginalPosition()
    {
        if (currentDropArea != null)
        {
            // If the object was in a drop area, mark the drop area as not occupied
            currentDropArea.isOccupied = false;
            currentDropArea.placedItem = null;
            currentDropArea = null;
        }

        // Reset the position of the dragged item
        transform.position = originalPosition;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
