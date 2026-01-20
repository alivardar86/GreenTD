using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;

    [SerializeField] private GameObject panel;

    private void Awake()
    {
        Instance = this;
        Hide(); // Oyun başında panel kapalı olsun
    }

    public void Show()
    {
        if (panel != null)
            panel.SetActive(true);

        // Oyunu durdur
        Time.timeScale = 0f;
    }

    public void Hide()
    {
        if (panel != null)
            panel.SetActive(false);

        // Zamanı normale döndür
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        // Restart'tan önce zamanı normale al
        Time.timeScale = 1f;

        // Aktif sahneyi yeniden yükle
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}
