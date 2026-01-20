using UnityEngine;

/// <summary>
/// Manages the base/castle health and game over condition
/// </summary>
public class BaseHealth : MonoBehaviour
{
    [SerializeField] private int startingLives = 10;
    
    private int currentLives;
    private bool isGameOver = false;

    private void Start()
    {
        currentLives = startingLives;
        
        if (LivesUI.Instance != null)
        {
            LivesUI.Instance.SetLives(currentLives);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only process enemies
        if (!collision.CompareTag(GameTags.Enemy))
            return;

        // Destroy enemy even after game over to prevent stacking
        if (isGameOver)
        {
            Destroy(collision.gameObject);
            return;
        }

        // Take damage
        currentLives--;

        if (currentLives <= 0)
        {
            HandleGameOver();
        }
        else
        {
            UpdateUI();
        }

        Destroy(collision.gameObject);
    }

    private void HandleGameOver()
    {
        currentLives = 0;
        isGameOver = true;

        UpdateUI();
        
        Debug.Log("GAME OVER!");

        if (GameOverUI.Instance != null)
        {
            GameOverUI.Instance.Show();
        }
    }

    private void UpdateUI()
    {
        if (LivesUI.Instance != null)
        {
            LivesUI.Instance.SetLives(currentLives);
        }
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}