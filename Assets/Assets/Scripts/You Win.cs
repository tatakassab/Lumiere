using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        unlockNewLevel();
        GameManager.instance.PlayerFinishedLevel();
    }

    void unlockNewLevel(){

    if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
    {
    PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
    PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
    PlayerPrefs.Save();
    
    }
    }
}
