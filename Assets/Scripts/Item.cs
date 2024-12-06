using UnityEngine;

public class Item
{
    // Propri�t�s de base
    public int Quantity { get; private set; }
    public int FirstValue { get; private set; }
    public int RevenuPerSecond { get; private set; }

    // Constructeur : Initialise les valeurs par d�faut
    public Item(int quantity, int firstValue, int revenuPerSecond)
    {
        Quantity = quantity;
        FirstValue = firstValue; // Valeur par clic
        RevenuPerSecond = revenuPerSecond; // Co�t initial pour une am�lioration
    }

    // M�thode calculant le cout de l'objet
    public int GetCost()
    {
        return FirstValue * (Quantity + 1);
    }

    // M�thode pour acheter un objet
    public void BuyObject()
    {
        Quantity++;
    }
}
