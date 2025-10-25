using UnityEngine;

public class RestartGame : MonoBehaviour
{

    public void LoadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level001");
        Time.timeScale = 1;
    }
    
}
