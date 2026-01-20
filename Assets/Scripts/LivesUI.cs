using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public static LivesUI Instance;

    [Header("UI References")]
    public TMP_Text livesText;
    public TMP_Text goldText;
    public TMP_Text waveText;

    private void Awake()
    {
        Instance = this;
    }

    public void SetLives(int lives)
    {
        if (livesText != null)
            livesText.text = "Lives: " + lives;
    }

    public void SetGold(int gold)
    {
        if (goldText != null)
            goldText.text = "Gold: " + gold;
    }

    public void SetWave(int wave)
    {
        if (waveText != null)
            waveText.text = "Wave: " + wave;
    }
}
