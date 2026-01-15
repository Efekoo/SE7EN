using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject panel;
    public string gameplaySceneName = "Level1";
    public string mainMenuSceneName = "MainMenu";

    public void Show()
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Single);
    }
}
