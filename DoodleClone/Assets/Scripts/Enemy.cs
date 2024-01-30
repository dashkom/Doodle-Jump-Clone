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
            // ”ничтожаем противника при попадании пули
            Destroy(gameObject);
            // ”ничтожаем пулю после попадани€ в противника
            Destroy(other.gameObject);
        }
    }
    private void Update()
    {
        // ѕровер€ем, если враг ниже камеры, то удал€ем ее
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
        {
            Destroy(gameObject);
        }
    }
}
