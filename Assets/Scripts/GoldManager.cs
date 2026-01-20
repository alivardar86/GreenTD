using UnityEngine;

/// <summary>
/// Manages player's gold (currency) throughout the game
/// </summary>
public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }

    [SerializeField] private int startingGold = 0;
    
    private int gold;

    private void Awake()
    {
        // Singleton pattern with duplicate prevention
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate GoldManager detected! Destroying this instance.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // Initialize gold after all Awake() calls are done
        gold = startingGold;
        UpdateUI();
    }

    public void AddGold(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Cannot add negative gold. Use TrySpendGold instead.");
            return;
        }
        
        gold += amount;
        UpdateUI();
    }

    public bool TrySpendGold(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Cannot spend negative gold.");
            return false;
        }

        if (gold < amount)
            return false;

        gold -= amount;
        UpdateUI();
        return true;
    }

    public int GetCurrentGold()
    {
        return gold;
    }

    private void UpdateUI()
    {
        if (LivesUI.Instance != null)
        {
            LivesUI.Instance.SetGold(gold);
        }
    }
}