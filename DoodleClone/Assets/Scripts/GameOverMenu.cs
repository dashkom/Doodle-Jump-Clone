using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    public void ShowGameOverMenu(int score)
    {
        gameObject.SetActive(true); // �������� ����� � ����
        scoreText.text = "Score: " + score.ToString();
    }

    public void OnPlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnShowRecordsButton()
    {
        // �������� ��� ��� ����������� ������ � ���������, ���� ����������
        // ��������, SceneManager.LoadScene("RecordsScene");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
