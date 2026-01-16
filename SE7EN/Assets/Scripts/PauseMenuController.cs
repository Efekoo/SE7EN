using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    public static PauseMenuController instance;


    public GameObject pauseMenuUI;


    bool isPaused = false;


    void Awake()
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

    }


    void Start()
    {

        // Oyun basladiginda menu kapali olsun
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

    }


    void Update()
    {

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {

            if (isPaused == true)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }

    }


    public void PauseGame()
    {

        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;

        isPaused = true;

    }


    public void ResumeGame()
    {

        pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;

        isPaused = false;

    }


    public void ReturnToMenu()
    {

        Time.timeScale = 1f;

        // Ana menüye (0 numaralı sahne) dön
        SceneManager.LoadScene(0);

        // Menüye dönünce bu pause objesini yok et ki menüde arkada çalışmasın
        Destroy(gameObject);

    }

}