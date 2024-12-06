using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CheckpointType checkpointType;
    public CheckpointSpecial checkpointSpecial;
    public Checkpoint nextCheckpoint;

    private void OnDrawGizmos()
    {
        // Set the Gizmo color to red
        Gizmos.color = Color.red;

        // Draw a filled sphere (which looks like a rounded marker) at the checkpoint's position
        // The size of the sphere can be adjusted with the second parameter (e.g., 0.5f)
        Gizmos.DrawSphere(transform.position, 0.2f);

        // If the next checkpoint exists, draw a line to it
        if (nextCheckpoint != null)
        {
            // Draw a line from this checkpoint to the next checkpoint
            Gizmos.DrawLine(transform.position, nextCheckpoint.transform.position);
        }
    }
}
