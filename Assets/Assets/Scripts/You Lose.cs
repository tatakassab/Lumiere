using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLose : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(2);
    }
}
