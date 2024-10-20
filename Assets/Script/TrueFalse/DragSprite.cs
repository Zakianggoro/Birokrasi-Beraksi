using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSprite : MonoBehaviour
{
    private bool isDragging = false;  // Tracks if the sprite is being dragged
    private Vector3 offset;           // Offset between the mouse position and sprite
    private Vector3 screenPoint;      // Stores screen point of the object
    private Rigidbody2D rb;           // Reference to Rigidbody2D for physics interactions

    public int typeNo = -1;
    public float dragSpeed = 10f;     // Controls how fast the object moves when dragged

    private void Start()
    {
        // Get the Rigidbody2D component attached to the sprite
        rb = GetComponent<Rigidbody2D>();

        // Ensure gravity is off if you don't want it
        rb.gravityScale = 0;

        // Optionally adjust drag values to control how it slows down after movement
        rb.drag = 5;  // Adjust as needed
        rb.angularDrag = 5;
    }

    private void OnMouseDown()
    {
        // When the mouse is clicked on the sprite, start dragging
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        isDragging = true;

        // Ensure the velocity is reset when starting to drag
        rb.velocity = Vector2.zero;
    }

    private void OnMouseDrag()
    {
        // While the mouse is dragging, update the sprite's position using Rigidbody2D's MovePosition
        if (isDragging)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;

            // Use Rigidbody2D.MovePosition to move the sprite while respecting colliders
            rb.MovePosition(Vector2.Lerp(rb.position, targetPosition, Time.deltaTime * dragSpeed));
        }
    }

    private void OnMouseUp()
    {
        // When the mouse is released, stop dragging
        isDragging = false;

        // Stop any residual movement by setting velocity to zero
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;  // Stop any rotation if it exists
    }
}