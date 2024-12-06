using UnityEngine;
using System;

public class Item
{
    // Propri�t�s de base
    public int Quantity { get; private set; }
    public int FirstValue { get; private set; }
    public int RevenuPerSecond { get; private set; }
    public int Efficiency { get; private set; }
    public int IncomePerSecond => Quantity * RevenuPerSecond * Efficiency;

    // Constructeur : Initialise les valeurs par d�faut
    public Item(int quantity, int firstValue, int revenuPerSecond)
    {
        Quantity = quantity;
        FirstValue = firstValue; // Valeur par clic
        RevenuPerSecond = revenuPerSecond; // Co�t initial pour une am�lioration
        Efficiency = 1; // Valeur en pourcentage
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

    // M�thode pour am�liorer un objet
    public void UpgradeObject()
    {
        Efficiency++;
    }

    // M�thode pour calculant le cout de l'am�lioration
    public int GetUpgradeCost()
    {
        return (int)(FirstValue * Math.Pow(3, Efficiency + 1));
    }
}
