using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    public void ShowGameOverMenu(int score)
    {
        gameObject.SetActive(true); // Включаем экран с меню
        scoreText.text = "Score: " + score.ToString();
    }

    public void OnPlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnShowRecordsButton()
    {
        // Добавьте код для отображения экрана с рекордами, если необходимо
        // Например, SceneManager.LoadScene("RecordsScene");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
