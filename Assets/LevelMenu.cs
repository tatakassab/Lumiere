using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelMenu : MonoBehaviour
{

public void OpenLevel(int levelId)
{
    string levelName = "Level " + levelId;
    SceneManager.LoadScene(levelName);

}
}