using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    Text highScoreText;

    [SerializeField]
    Text scoreText;

    int highScore;
    int score;
    void Start()
    {
        highScore = PlayerPrefs.GetInt("scoreMemory");
        highScoreText.text = "High Score: " + highScore;

        score = PlayerPrefs.GetInt("currentScore");
        scoreText.text = "Score: " + score;
    }

    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
