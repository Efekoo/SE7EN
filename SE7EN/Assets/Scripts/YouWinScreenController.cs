using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinScreenController : MonoBehaviour
{
    public GameObject youWinScreenUI;

    void Awake()
    {
        // Oyun başında ekranı gizle
        if (youWinScreenUI != null)
        {
            youWinScreenUI.SetActive(false);
        }
    }

    public void Show()
    {
        if (youWinScreenUI != null)
        {
            youWinScreenUI.SetActive(true);
            // İsteğe bağlı: Oyunu durdurmak için
            Time.timeScale = 0f;
        }
    }

    public void ReturnToMenu()
    {
        // Oyunu tekrar normal hızına getir
        Time.timeScale = 1f;
        // Ana menü sahnesini yükle
        SceneManager.LoadScene("MainMenu");
    }
}
