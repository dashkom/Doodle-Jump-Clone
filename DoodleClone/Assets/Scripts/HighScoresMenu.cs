using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoresMenu : MonoBehaviour
{
    public Text[] highScoreTexts;

    void Start()
    {
        DisplayHighScores();
    }

    void DisplayHighScores()
    {
        for (int i = 0; i < highScoreTexts.Length; i++)
        {
            int score = PlayerPrefs.GetInt("HighScore" + i, 0);

            if (score > 0)
            {
                highScoreTexts[i].text = (i + 1) + ". " + score.ToString();
            }
            else
            {
                highScoreTexts[i].text = "";
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}