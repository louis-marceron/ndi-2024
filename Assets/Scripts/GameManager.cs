using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    }

    void Update()
    {
        // Mise à jour de l'argent
        Money += IncomePerSecond() * Time.deltaTime;
        moneyText.text = "Money : " + Money.ToString("F2") + "$";
        incomeText.text = "Income : " + IncomePerSecond() + "$/s";
        fishermanText.text = "Fisherman : " + fisherman.Quantity + " (+" + fisherman.RevenuPerSecond + "$/c)" + " (cost: " + fisherman.GetCost() + "$)";
        boatText.text = "Boat : " + boat.Quantity + " (+" + boat.RevenuPerSecond + "$/s)" + " (cost: " + boat.GetCost() + "$)";
        factoryText.text = "Factory : " + factory.Quantity + " (+" + factory.RevenuPerSecond + "$/s)" + " (cost: " + factory.GetCost() + "$)";
        storeText.text = "Store : " + store.Quantity + " (+" + store.RevenuPerSecond + "$/s)" + " (cost: " + store.GetCost() + "$)";
    }

    // Méthode pour gérer l'argent
    public int IncomePerSecond()
    {
        return boat.RevenuPerSecond * boat.Quantity + factory.RevenuPerSecond * factory.Quantity + store.RevenuPerSecond * store.Quantity;
    }

    // Méthode pour gérer un clic
    public int Click()
    {
        return fisherman.RevenuPerSecond * fisherman.Quantity;
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
}