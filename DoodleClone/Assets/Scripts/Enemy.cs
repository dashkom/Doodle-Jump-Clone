using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Death.instance.EndGame();
        }
        else if (other.CompareTag("Bullet"))
        {
            // ���������� ���������� ��� ��������� ����
            Destroy(gameObject);
            // ���������� ���� ����� ��������� � ����������
            Destroy(other.gameObject);
        }
    }
    private void Update()
    {
        // ���������, ���� ���� ���� ������, �� ������� ��
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
        {
            Destroy(gameObject);
        }
    }
}
