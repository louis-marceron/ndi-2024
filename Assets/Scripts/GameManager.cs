using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    // Singleton Instance
    public static GameManager Instance { get; private set; }

    // Propriétés de base
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
            Instance = this; // Définir l'instance
            DontDestroyOnLoad(gameObject); // Ne pas détruire entre les scènes
        }
        else
        {
            Destroy(gameObject); // Détruire les duplicatas
        }
    }

    // Constructeur : Initialise les valeurs par défaut
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
        // Mise à jour de l'argent
        Money += IncomePerSecond() * Time.deltaTime;
        moneyText.text = "Argent : " + Math.Round(Money).ToString() + "$";
        incomeText.text = "Revenue : " + IncomePerSecond() + "$/sec";
        fishermanText.text = "Pêcheur : " + fisherman.Quantity + " (+" + fisherman.RevenuPerSecond + "$/click)" + " (cout: " + fisherman.GetCost() + "$)" + " (efficacité: " + fisherman.Efficiency*100 + "%)";
        boatText.text = "Bateau : " + boat.Quantity + " (+" + boat.RevenuPerSecond + "$/sec)" + " (cout: " + boat.GetCost() + "$)" + " (efficacité: " + boat.Efficiency*100 + "%)";
        factoryText.text = "Usine : " + factory.Quantity + " (+" + factory.RevenuPerSecond + "$/sec)" + " (cout: " + factory.GetCost() + "$)" + " (efficacité: " + factory.Efficiency*100 + "%)";
        storeText.text = "Magasin : " + store.Quantity + " (+" + store.RevenuPerSecond + "$/sec)" + " (cout: " + store.GetCost() + "$)" + " (efficacité: " + store.Efficiency*100 + "%)";
        fishmanUpgradeButton.GetComponentInChildren<TMP_Text>().text = "Améliorer (cout: " + fisherman.GetUpgradeCost() + "$)";
        boatUpgradeButton.GetComponentInChildren<TMP_Text>().text = "Améliorer (cout: " + boat.GetUpgradeCost() + "$)";
        factoryUpgradeButton.GetComponentInChildren<TMP_Text>().text = "Améliorer (cout: " + factory.GetUpgradeCost() + "$)";
        storeUpgradeButton.GetComponentInChildren<TMP_Text>().text = "Améliorer (cout: " + store.GetUpgradeCost() + "$)";
    }

    // Méthode pour gérer l'argent
    public int IncomePerSecond()
    {
        return boat.IncomePerSecond + factory.IncomePerSecond + store.IncomePerSecond;
    }

    // Méthode pour gérer un clic
    public int Click()
    {
        return fisherman.IncomePerSecond;
    }

    // Méthode pour acheter un objet
    public void BuyObject(Item obj)
    {
        if (Money >= obj.GetCost())
        {
            Money -= obj.GetCost();
            obj.BuyObject();
        }
    }

    // Méthode pour améliorer un objet
    public void UpgradeObject(Item obj)
    {
        if (Money >= obj.GetUpgradeCost())
        {
            Money -= obj.GetUpgradeCost();
            obj.UpgradeObject();
        }
    }
}