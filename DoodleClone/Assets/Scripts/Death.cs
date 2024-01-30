using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public static Death instance;
    public Transform doodleTransform;
    public float fallThreshold = -8f; // ����� �������, ��� ������� ���� ����������
    public GameOverMenu gameOverMenu;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        // ��������� ������ �����
        if (doodleTransform.position.y < fallThreshold)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        // �������� ������� ����
        int currentScore = Doodle.instance.GetScore();

        PlayerPrefs.SetInt("CurrentScore", currentScore);
        PlayerPrefs.Save();  // ��������� ���������

        List<int> highScores = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            int score = PlayerPrefs.GetInt("HighScore" + i, 0);
            highScores.Add(score);
        }

        // ��������� ������� ���� � ������ ��������
        highScores.Add(currentScore);

        // ��������� ������ �� ��������
        highScores.Sort((a, b) => b.CompareTo(a));

        // ��������� ������ 6 ����������� � PlayerPrefs
        for (int i = 0; i < 6; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }

        PlayerPrefs.Save();  // ��������� ���������

        SceneManager.LoadScene("Death");
    }
}
