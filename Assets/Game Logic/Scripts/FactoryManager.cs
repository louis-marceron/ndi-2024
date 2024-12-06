using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    public GameObject factoryPrefab1;
    public GameObject factoryPrefab2;
    public GameObject factoryPrefab3;
    public int minimalQuantity = 1;

    private GameObject child;
    private int currentStep; // Tracks the current step (1, 2, or 3)

    private void Start()
    {
        currentStep = GetStep(); // Initialize current step based on starting conditions
        InstantiateFactoryForStep(currentStep); // Instantiate the correct boat
    }

    void Update()
    {
        int newStep = GetStep(); // Determine the current step based on game state

        if (newStep != currentStep) // Only update if the step has changed
        {
            currentStep = newStep; // Update the current step
            InstantiateFactoryForStep(currentStep); // Update the boat prefab
        }
    }

    private int GetStep()
    {
        if (GameManager.Instance.factory == null)
        {
            return 0;
        }

        // Determine the step based on efficiency and quantity
        if (GameManager.Instance.factory.Quantity >= minimalQuantity && GameManager.Instance.factory.Efficiency >= 3)
        {
            return 3; // Step 3: Use factoryPrefab3
        }
        else if (GameManager.Instance.factory.Quantity >= minimalQuantity && GameManager.Instance.factory.Efficiency >= 2)
        {
            return 2; // Step 2: Use factoryPrefab2
        }
        else if (GameManager.Instance.factory.Quantity >= minimalQuantity)
        {
            return 1; // Step 1: Use factoryPrefab1
        }

        return 0; // Default step (no factory), if needed
    }

    private void InstantiateFactoryForStep(int step)
    {
        if (child != null)
        {
            Destroy(child.gameObject); // Destroy the current factory
        }

        // Instantiate the correct factory based on the step
        switch (step)
        {
            case 1:
                child = Instantiate(factoryPrefab1, transform);
                break;
            case 2:
                child = Instantiate(factoryPrefab2, transform);
                break;
            case 3:
                child = Instantiate(factoryPrefab3, transform);
                break;
            default:
                child = null; // No factory for step 0
                break;
        }
    }
}