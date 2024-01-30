using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public static Death instance;
    public Transform doodleTransform;
    public float fallThreshold = -8f; // Порог падения, при котором игра завершится
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
        // Проверяем высоту дудла
        if (doodleTransform.position.y < fallThreshold)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        // Получаем текущий счет
        int currentScore = Doodle.instance.GetScore();

        PlayerPrefs.SetInt("CurrentScore", currentScore);
        PlayerPrefs.Save();  // Сохраняем изменения

        List<int> highScores = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            int score = PlayerPrefs.GetInt("HighScore" + i, 0);
            highScores.Add(score);
        }

        // Добавляем текущий счет в массив рекордов
        highScores.Add(currentScore);

        // Сортируем массив по убыванию
        highScores.Sort((a, b) => b.CompareTo(a));

        // Сохраняем первые 6 результатов в PlayerPrefs
        for (int i = 0; i < 6; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }

        PlayerPrefs.Save();  // Сохраняем изменения

        SceneManager.LoadScene("Death");
    }
}
