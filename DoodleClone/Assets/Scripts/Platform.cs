using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float jumpForce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 vel = rb.velocity;
            vel.y = jumpForce;
            rb.velocity = vel;
        }
    }
    private void Update()
    {
        // Проверяем, если платформа ниже камеры, то удаляем ее
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
        {
            Destroy(gameObject);
        }
    }
}
