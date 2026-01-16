using UnityEngine;
using TMPro; 

public class ScoreManager : MonoBehaviour
{

    public int score = 0;


  
    public TextMeshProUGUI scoreText;


    public void AddScore(int amount)
    {

        score = score + amount;

        UpdateScoreText();

    }


    void Start()
    {

        UpdateScoreText();

    }


    void UpdateScoreText()
    {

        if (scoreText != null)
        {
            scoreText.text = "Souls: " + score;
        }

    }

}