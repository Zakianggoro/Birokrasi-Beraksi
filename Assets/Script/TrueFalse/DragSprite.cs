using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSprite : MonoBehaviour
{
    private bool isDragging = false;  // Tracks if the sprite is being dragged
    private Vector3 offset;           // Offset between the mouse position and sprite
    private Vector3 screenPoint;      // Stores screen point of the object
    private Rigidbody2D rb;           // Reference to Rigidbody2D for physics interactions
    private bool isInsideTrigger = false;  // Tracks if the sprite is inside the trigger area
    private ScoreTrigger currentTrigger;  // Reference to the trigger the sprite is inside

    public Transform spriteTransform; // Reference to the child object's transform (visual sprite)
    public int typeNo = -1;
    public float dragSpeed = 10f;     // Controls how fast the object moves when dragged
    public float shrinkFactor = 0.7f; // Factor by which the visual sprite shrinks in size when inside trigger
    private Vector3 originalScale;    // Stores the original scale of the child object (visual sprite)

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ensure gravity is off if you don't want it
        rb.gravityScale = 0;
        rb.drag = 5;  // Adjust as needed
        rb.angularDrag = 5;

        // Store the original scale of the child object (visual sprite)
        originalScale = spriteTransform.localScale;
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

        // Check if sprite is still inside the trigger area when released
        if (isInsideTrigger)
        {
            // Trigger the original functionality (score increase, deactivation)
            currentTrigger.CheckScoreOnRelease(this);
        }

        // Always reset the visual sprite scale when mouse is released
        spriteTransform.localScale = originalScale;

        // Stop any residual movement by setting velocity to zero
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;  // Stop any rotation if it exists
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreTrigger"))  // Replace with your trigger object's tag
        {
            isInsideTrigger = true;

            // Store the trigger we're currently inside
            currentTrigger = other.GetComponent<ScoreTrigger>();

            // Shrink the child object (visual sprite) when it enters the trigger area
            spriteTransform.localScale = originalScale * shrinkFactor;
            Debug.Log("Sprite entered the trigger area and shrunk.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ScoreTrigger"))  // Replace with your trigger object's tag
        {
            isInsideTrigger = false;

            // Clear the trigger reference
            currentTrigger = null;

            // Reset the visual sprite scale when it exits the trigger area
            spriteTransform.localScale = originalScale;
            Debug.Log("Sprite exited the trigger area, returning to original size.");
        }
    }
}
