using UnityEngine;

public class Spawned : MonoBehaviour
{
    public Checkpoint nextCheckpoint; // The current target checkpoint
    public float speed = 5f; // Movement speed
    public float checkpointProximityThreshold = 0.02f; // Distance to consider "reaching" the checkpoint

    private void Update()
    {
        // Move towards the next checkpoint if it's set
        if (nextCheckpoint != null)
        {
            // Move towards the next checkpoint
            Vector3 direction = (nextCheckpoint.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Optional: Rotate towards the checkpoint (for aesthetics)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Check the distance to the checkpoint
            if (Vector3.Distance(transform.position, nextCheckpoint.transform.position) <= checkpointProximityThreshold)
            {
                Checkpoint checkpointScript = nextCheckpoint.GetComponent<Checkpoint>();
                if (checkpointScript.checkpointSpecial == CheckpointSpecial.CHANGE_BEHAVIOUR)
                {
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    spriteRenderer.color = Color.blue;
                }
                else if (checkpointScript.checkpointSpecial == CheckpointSpecial.END)
                {
                    Destroy(gameObject);
                }
                // Update the next checkpoint
                nextCheckpoint = nextCheckpoint.nextCheckpoint;
            }
        }
    }
}