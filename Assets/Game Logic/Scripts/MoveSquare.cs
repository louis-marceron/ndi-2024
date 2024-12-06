using UnityEngine;

public class MoveSquare : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move the object 1 unit per second on the x-axis
        transform.position += Vector3.right * Time.deltaTime;
    }
}