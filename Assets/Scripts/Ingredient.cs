using UnityEngine;

public class Ingredient : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 startingPosition; // Store the initial position
    private Transform t;
    private BoxCollider2D c;

    public enum COLOR { NO_COLOR = -1, YELLOW, RED, BLUE, PURPLE }
    public COLOR color = 0;

    void Start()
    {
        t = transform;
        c = GetComponent<BoxCollider2D>();
        startingPosition = t.position; // Save the starting position
    }

    void Update()
    {
        if (isDragging)
        {
            // Follow the mouse in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the z-coordinate matches your 2D plane
            t.position = mousePosition;
        }
    }

    void OnMouseDown()
    {
        // Start dragging when clicked
        isDragging = true;
    }

    void OnMouseUp()
    {
        // Stop dragging
        isDragging = false;

        // Snap to nearest grid point or return to starting position
        if (IsValidDropPosition(t.position))
        {
            t.position = SnapToGrid(t.position);
        }
        else
        {
            t.position = startingPosition;
        }
    }

    private Vector3 SnapToGrid(Vector3 position)
    {
        // Snap the position to the nearest whole number (grid size of 1x1)
        float snappedX = Mathf.Round(position.x);
        float snappedY = Mathf.Round(position.y);
        return new Vector3(snappedX, snappedY, position.z);
    }

    private bool IsValidDropPosition(Vector3 position)
    {
        float minX = 0f, maxX = 10f;
        float minY = 0f, maxY = 10f;

        return position.x >= minX && position.x <= maxX && position.y >= minY && position.y <= maxY;
    }
}
