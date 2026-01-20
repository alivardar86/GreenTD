using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the Game Over UI panel and restart functionality
/// </summary>
public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    [SerializeField] private GameObject panel;

    private void Awake()
    {
        // Singleton pattern with duplicate prevention
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate GameOverUI detected! Destroying this instance.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        Hide(); // Start with panel hidden
    }

    public void Show()
    {
        if (panel != null)
            panel.SetActive(true);

        // Pause game
        Time.timeScale = 0f;
    }

    public void Hide()
    {
        if (panel != null)
            panel.SetActive(false);

        // Resume game
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        // Restore time scale before reloading
        Time.timeScale = 1f;

        // Reload current scene
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}