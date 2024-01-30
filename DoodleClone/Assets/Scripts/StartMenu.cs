using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Scores()
    {
        SceneManager.LoadScene("HighScores");
    }
    public void ExitGame()
    {
        Debug.Log("The game is quit");
        Application.Quit();
    }
}
