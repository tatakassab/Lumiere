using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.PlayerFinishedLevel();
    }
}
