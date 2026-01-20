using TMPro;
using UnityEngine;

/// <summary>
/// Manages the HUD display for lives, gold, and wave count
/// </summary>
public class LivesUI : MonoBehaviour
{
    public static LivesUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text waveText;

    private void Awake()
    {
        // Singleton pattern with duplicate prevention
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate LivesUI detected! Destroying this instance.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetLives(int lives)
    {
        if (livesText != null)
            livesText.text = $"Lives: {lives}";
    }

    public void SetGold(int gold)
    {
        if (goldText != null)
            goldText.text = $"Gold: {gold}";
    }

    public void SetWave(int wave)
    {
        if (waveText != null)
            waveText.text = $"Wave: {wave}";
    }
}