using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

    public int gold = 0;

    private void Awake()
    {
        Instance = this;
        // Oyun başında HUD'i güncelle
        LivesUI.Instance.SetGold(gold);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        LivesUI.Instance.SetGold(gold);
    }

    public bool TrySpendGold(int amount)
    {
        if (gold < amount)
            return false;

        gold -= amount;
        LivesUI.Instance.SetGold(gold);
        return true;
    }
}
