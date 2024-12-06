using UnityEngine;
using System;

public class Item
{
    // Propriétés de base
    public int Quantity { get; private set; }
    public int FirstValue { get; private set; }
    public int RevenuPerSecond { get; private set; }
    public int Efficiency { get; private set; }
    public int IncomePerSecond => Quantity * RevenuPerSecond * Efficiency;

    // Constructeur : Initialise les valeurs par défaut
    public Item(int quantity, int firstValue, int revenuPerSecond)
    {
        Quantity = quantity;
        FirstValue = firstValue; // Valeur par clic
        RevenuPerSecond = revenuPerSecond; // Coût initial pour une amélioration
        Efficiency = 1; // Valeur en pourcentage
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

    // Méthode pour améliorer un objet
    public void UpgradeObject()
    {
        Efficiency++;
    }

    // Méthode pour calculant le cout de l'amélioration
    public int GetUpgradeCost()
    {
        return (int)(FirstValue * Math.Pow(3, Efficiency + 1));
    }
}
