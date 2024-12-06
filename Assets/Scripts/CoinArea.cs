using UnityEngine;

public class CoinArea : MonoBehaviour
{
    public float x_ellipse = 5f; // Semi-major axis of the ellipse (horizontal radius)
    public float y_ellipse = 3f; // Semi-minor axis of the ellipse (vertical radius)
    public GameObject prefab; // The prefab to instantiate
    public float duration = 2f; // Duration for which the prefab will exist (in seconds)
    public float forceAmount = 5f; // The amount of force to apply to the prefab (in the upward direction)

    // Method to instantiate the prefab at a random position within the ellipse area
    public void InstantiatePrefabAtRandomPosition()
    {
        // Generate a random value in the range -1 to 1 for both x and y axes
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        // Scale the random values by the ellipse's radii to get points inside the ellipse
        float xPos = randomX * x_ellipse;
        float yPos = randomY * y_ellipse;

        // Check if the point is inside the ellipse using the ellipse equation
        if ((xPos * xPos) / (x_ellipse * x_ellipse) + (yPos * yPos) / (y_ellipse * y_ellipse) <= 1f)
        {
            // Calculate the final position relative to the object's position
            Vector3 position = new Vector3(xPos, yPos, -1f) + transform.position;

            // Instantiate the prefab at the calculated position
            if (prefab != null)
            {
                GameObject instantiatedPrefab = Instantiate(prefab, position, Quaternion.identity);

                // Ensure the instantiated prefab has a Rigidbody2D component
                Rigidbody2D rb = instantiatedPrefab.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Apply a 2D force in the upward direction (positive Y-axis)
                    rb.AddForce(Vector2.up * forceAmount, ForceMode2D.Impulse);
                }
                else
                {
                    Debug.LogWarning("The instantiated prefab does not have a Rigidbody2D component!");
                }

                // Destroy the prefab after the specified duration
                Destroy(instantiatedPrefab, duration);
            }
            else
            {
                Debug.LogWarning("Prefab not assigned in the CoinArea script!");
            }
        }
    }

    // This method is used to draw gizmos in the scene view (optional for visualization)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Set the color of the gizmo

        // To draw an ellipse, we will sample points along the circumference
        int segments = 100; // Number of points to draw the ellipse (higher number = smoother)
        Vector3 previousPoint = Vector3.zero;

        // Loop through the angle (in radians)
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * Mathf.PI * 2 / segments; // Divide the circle into segments
            float x = Mathf.Cos(angle) * x_ellipse;
            float y = Mathf.Sin(angle) * y_ellipse;

            Vector3 currentPoint = new Vector3(x, y, 0f) + transform.position;

            // Draw lines between consecutive points
            if (i > 0)
            {
                Gizmos.DrawLine(previousPoint, currentPoint);
            }

            previousPoint = currentPoint;
        }
    }
}