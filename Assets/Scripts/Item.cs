using UnityEngine;

public class Item
{
    // Propriétés de base
    public int Quantity { get; private set; }
    public int FirstValue { get; private set; }
    public int RevenuPerSecond { get; private set; }

    // Constructeur : Initialise les valeurs par défaut
    public Item(int quantity, int firstValue, int revenuPerSecond)
    {
        Quantity = quantity;
        FirstValue = firstValue; // Valeur par clic
        RevenuPerSecond = revenuPerSecond; // Coût initial pour une amélioration
    }

    // Méthode calculant le cout de l'objet
    public int GetCost()
    {
        return FirstValue * (Quantity + 1);
    }

    // Méthode pour acheter un objet
    public void BuyObject()
    {
        Quantity++;
    }
}
