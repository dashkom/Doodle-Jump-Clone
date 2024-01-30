using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundsController : MonoBehaviour
{
    private void Update()
    {
        CheckOutOfBounds();
    }

    private void CheckOutOfBounds()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPos.x < 0)
        {
            // Перемещаем объект вправо за пределы экрана
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1, viewPos.y, viewPos.z));
        }
        else if (viewPos.x > 1)
        {
            // Перемещаем объект влево за пределы экрана
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0, viewPos.y, viewPos.z));
        }
    }
}
