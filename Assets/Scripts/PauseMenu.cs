using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(true);
            Time.timeScale = 0f;

        }
        
    }

    public void ResumeButton()
    {
        container.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("");
    }
}
