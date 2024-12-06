using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public GameObject boatPrefab1;
    public GameObject boatPrefab2;
    public GameObject boatPrefab3;
    public int minimalQuantity = 1;

    private GameObject child;
    private int currentStep; // Tracks the current step (1, 2, or 3)

    private void Start()
    {
        currentStep = GetStep(); // Initialize current step based on starting conditions
        InstantiateBoatForStep(currentStep); // Instantiate the correct boat
    }

    void Update()
    {
        int newStep = GetStep(); // Determine the current step based on game state

        if (newStep != currentStep) // Only update if the step has changed
        {
            currentStep = newStep; // Update the current step
            InstantiateBoatForStep(currentStep); // Update the boat prefab
        }
    }

    private int GetStep()
    {
        if (GameManager.Instance.boat == null)
        {
            return 0;
        }

        // Determine the step based on efficiency and quantity
        if (GameManager.Instance.boat.Quantity >= minimalQuantity && GameManager.Instance.boat.Efficiency >= 3)
        {
            return 3; // Step 3: Use boatPrefab3
        }
        else if (GameManager.Instance.boat.Quantity >= minimalQuantity && GameManager.Instance.boat.Efficiency >= 2)
        {
            return 2; // Step 2: Use boatPrefab2
        }
        else if (GameManager.Instance.boat.Quantity >= minimalQuantity)
        {
            return 1; // Step 1: Use boatPrefab1
        }

        return 0; // Default step (no boat), if needed
    }

    private void InstantiateBoatForStep(int step)
    {
        if (child != null)
        {
            Destroy(child.gameObject); // Destroy the current boat
        }

        // Instantiate the correct boat based on the step
        switch (step)
        {
            case 1:
                child = Instantiate(boatPrefab1, transform);
                break;
            case 2:
                child = Instantiate(boatPrefab2, transform);
                break;
            case 3:
                child = Instantiate(boatPrefab3, transform);
                break;
            default:
                child = null; // No boat for step 0
                break;
        }
    }
}