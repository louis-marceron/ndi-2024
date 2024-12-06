using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    // Singleton Instance
    public static GameManager Instance { get; private set; }

    // Propri?t?s de base
    public float Money;
    public Item fisherman; // Reference to the Item
    public Item boat; // Reference to the Item
    public Item factory; // Reference to the Item
    public Item store; // Reference to the Item

    // Reference to the UI
    public TMP_Text moneyText;
    public TMP_Text incomeText;
    public TMP_Text fishermanText;
    public TMP_Text boatText;
    public TMP_Text factoryText;
    public TMP_Text storeText;
    public Button clickButton;
    public Button fishermanButton;
    public Button boatButton;
    public Button factoryButton;
    public Button storeButton;
    public Button fishmanUpgradeButton;
    public Button boatUpgradeButton;
    public Button factoryUpgradeButton;
    public Button storeUpgradeButton;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this; // D?finir l'instance
            DontDestroyOnLoad(gameObject); // Ne pas d?truire entre les sc?nes
        }
        else
        {
            Destroy(gameObject); // D?truire les duplicatas
        }
    }

    // Constructeur : Initialise les valeurs par d?faut
    void Start()
    {
        Money = 0;
        fisherman = new Item(1, 5, 1); // Valeur par clic
        boat = new Item(0, 25, 5); // Valeur par seconde
        factory = new Item(0, 50, 5); // Valeur par seconde
        store = new Item(0, 100, 10); // Valeur par seconde

        // Initialisation des boutons
        clickButton.onClick.AddListener(() => Money += Click());
        fishermanButton.onClick.AddListener(() => BuyObject(fisherman));
        boatButton.onClick.AddListener(() => BuyObject(boat));
        factoryButton.onClick.AddListener(() => BuyObject(factory));
        storeButton.onClick.AddListener(() => BuyObject(store));
        fishmanUpgradeButton.onClick.AddListener(() => UpgradeObject(fisherman));
        boatUpgradeButton.onClick.AddListener(() => UpgradeObject(boat));
        factoryUpgradeButton.onClick.AddListener(() => UpgradeObject(factory));
        storeUpgradeButton.onClick.AddListener(() => UpgradeObject(store));
    }

    void Update()
    {
        // Mise ? jour de l'argent
        Money += IncomePerSecond() * Time.deltaTime;
        moneyText.text = "Argent : " + Math.Round(Money).ToString() + "Euros";
        incomeText.text = "Revenu : " + IncomePerSecond() + "Euros/sec";
        fishermanText.text = "Pecheur : " + fisherman.Quantity + " (+" + fisherman.RevenuPerSecond + "Euros/click)" + " (cout: " + fisherman.GetCost() + "Euros)" + " (efficacite: " + fisherman.Efficiency*100 + "%)";
        boatText.text = "Bateau : " + boat.Quantity + " (+" + boat.RevenuPerSecond + "Euros/sec)" + " (cout: " + boat.GetCost() + "Euros)" + " (efficacite: " + boat.Efficiency*100 + "%)";
        factoryText.text = "Usine : " + factory.Quantity + " (+" + factory.RevenuPerSecond + "Euros/sec)" + " (cout: " + factory.GetCost() + "Euros)" + " (efficacite: " + factory.Efficiency*100 + "%)";
        storeText.text = "Magasin : " + store.Quantity + " (+" + store.RevenuPerSecond + "Euros/sec)" + " (cout: " + store.GetCost() + "Euros)" + " (efficacite: " + store.Efficiency*100 + "%)";
        fishmanUpgradeButton.GetComponentInChildren<TMP_Text>().text = "Ameliorer (cout: " + fisherman.GetUpgradeCost() + "Euros)";
        boatUpgradeButton.GetComponentInChildren<TMP_Text>().text = "Ameliorer (cout: " + boat.GetUpgradeCost() + "Euros)";
        factoryUpgradeButton.GetComponentInChildren<TMP_Text>().text = "Ameliorer (cout: " + factory.GetUpgradeCost() + "Euros)";
        storeUpgradeButton.GetComponentInChildren<TMP_Text>().text = "Ameliorer (cout: " + store.GetUpgradeCost() + "Euros)";

        if (fisherman.GetUpgradeCost() > Money) DisableButton(fishmanUpgradeButton);
        if (boat.GetUpgradeCost() > Money) DisableButton(boatUpgradeButton);
        if (factory.GetUpgradeCost() > Money) DisableButton(factoryUpgradeButton);
        if (store.GetUpgradeCost() > Money) DisableButton(storeUpgradeButton);
        if (fisherman.GetCost() > Money) DisableButton(fishermanButton);
        if (boat.GetCost() > Money) DisableButton(boatButton);
        if (factory.GetCost() > Money) DisableButton(factoryButton);
        if (store.GetCost() > Money) DisableButton(storeButton);

        if (fisherman.GetUpgradeCost() <= Money) EnableButton(fishmanUpgradeButton);
        if (boat.GetUpgradeCost() <= Money) EnableButton(boatUpgradeButton);
        if (factory.GetUpgradeCost() <= Money) EnableButton(factoryUpgradeButton);
        if (store.GetUpgradeCost() <= Money) EnableButton(storeUpgradeButton);
        if (fisherman.GetCost() <= Money) EnableButton(fishermanButton);
        if (boat.GetCost() <= Money) EnableButton(boatButton);
        if (factory.GetCost() <= Money) EnableButton(factoryButton);
        if (store.GetCost() <= Money) EnableButton(storeButton);
    }

    // M?thode pour g?rer l'argent
    public int IncomePerSecond()
    {
        return boat.IncomePerSecond + factory.IncomePerSecond + store.IncomePerSecond;
    }

    // M?thode pour g?rer un clic
    public int Click()
    {
        return fisherman.IncomePerSecond;
    }

    // M?thode pour acheter un objet
    public void BuyObject(Item obj)
    {
        if (Money >= obj.GetCost())
        {
            Money -= obj.GetCost();
            obj.BuyObject();
        }
    }

    // M?thode pour am?liorer un objet
    public void UpgradeObject(Item obj)
    {
        if (Money >= obj.GetUpgradeCost())
        {
            Money -= obj.GetUpgradeCost();
            obj.UpgradeObject();
        }
    }

    public void DisableButton(Button button)
    {
        button.interactable = false;
    }

    public void EnableButton(Button button)
    {
        button.interactable = true;
    }
}