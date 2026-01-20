using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int lives = 10;
    private bool isGameOver = false;

    private void Start()
    {
        LivesUI.Instance.SetLives(lives);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        if (isGameOver)
        {
            Destroy(collision.gameObject);
            return;
        }

        lives--;

        if (lives <= 0)
        {
            lives = 0;
            isGameOver = true;

            LivesUI.Instance.SetLives(lives);
            Debug.Log("GAME OVER!");

            if (GameOverUI.Instance != null)
            {
                GameOverUI.Instance.Show();
            }
        }
        else
        {
            LivesUI.Instance.SetLives(lives);
        }

        Destroy(collision.gameObject);
    }
}
