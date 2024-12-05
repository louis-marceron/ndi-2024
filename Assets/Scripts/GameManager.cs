using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject emplacementBateau1; // Reference to the GameObject
    public Sprite newSprite; // Sprite to apply to the GameObject

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Game manager started");
    }

    // Update is called once per frame
    void Update()
    {
        if (true) // Replace with your condition
        {
            // Apply the new sprite to the GameObject
            emplacementBateau1.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }
}