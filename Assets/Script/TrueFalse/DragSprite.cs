using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragSprite : MonoBehaviour
{
    private bool isDragging = false;
    private bool isHolding = false;   // New flag to track holding
    private Vector3 offset;
    private Vector3 screenPoint;
    private Rigidbody2D rb;
    private bool isInsideTrigger = false;
    private ScoreTrigger currentTrigger;
    public Transform spriteTransform;
    public int typeNo = -1;
    public float dragSpeed = 10f;
    public float shrinkFactor = 0.7f;
    private Vector3 originalScale;

    public float holdTimeThreshold = 0.2f;  // Time to wait before dragging
    private float holdTime = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.drag = 5;
        rb.angularDrag = 5;
        originalScale = spriteTransform.localScale;
    }

    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        isHolding = true;
        holdTime = 0f;

        rb.velocity = Vector2.zero;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
            rb.MovePosition(Vector2.Lerp(rb.position, targetPosition, Time.deltaTime * dragSpeed));
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            if (isInsideTrigger)
            {
                currentTrigger.CheckScoreOnRelease(this);
            }
            spriteTransform.localScale = originalScale;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            isDragging = false;
        }
        isHolding = false;
    }

    private void Update()
    {
        if (isHolding)
        {
            holdTime += Time.deltaTime;
            if (holdTime >= holdTimeThreshold)
            {
                isDragging = true;
                isHolding = false;  // Stop holding since drag has started
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreTrigger"))
        {
            isInsideTrigger = true;
            currentTrigger = other.GetComponent<ScoreTrigger>();
            spriteTransform.localScale = originalScale * shrinkFactor;
            Debug.Log("Sprite entered the trigger area and shrunk.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ScoreTrigger"))
        {
            isInsideTrigger = false;
            currentTrigger = null;
            spriteTransform.localScale = originalScale;
            Debug.Log("Sprite exited the trigger area, returning to original size.");
        }
    }

    public bool IsDragging()
    {
        return isDragging;
    }
}
