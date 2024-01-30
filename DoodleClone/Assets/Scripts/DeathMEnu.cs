using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMEnu : MonoBehaviour
{
    public static DeathMEnu instance;
    public Text scoreText;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        // Получаем текущий счет из PlayerPrefs
        int currentScore = PlayerPrefs.GetInt("CurrentScore", 0);

        // Отображаем счет в компоненте Text
        scoreText.text = "Your score: " + currentScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
