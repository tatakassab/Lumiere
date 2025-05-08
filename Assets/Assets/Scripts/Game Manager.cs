using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int maxLives = 3;
    private int currentLives;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentLives = maxLives;
    }

    public void PlayerDied()
    {
        currentLives--;

        if (currentLives > 0)
        {
            ReloadLevel();
        }
        else
        {
            LoadEndScreen();
        }
    }

    public void PlayerFinishedLevel()
    {
        int b = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(b);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadEndScreen()
    {
        SceneManager.LoadScene("You Lose");
    }
}
