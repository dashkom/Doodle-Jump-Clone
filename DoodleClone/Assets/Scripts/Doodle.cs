using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doodle : MonoBehaviour
{
    public static Doodle instance;
    public Rigidbody2D rbDoodle;
    private int score = 0;
    public Text scoreText;
    public Text highScoreText;
    public float maxSpeed = 5f;
    private GameObject lastPlatform;
    private bool isDrawingPlatform = false;
    private float touchStartTime;
    private float touchEndTime;
    private float minTouchDuration = 0.2f; // Минимальная длительность касания для стрельбы

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        //HandleTouchInput();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            // Проверяем, является ли текущая платформа новой
            if (lastPlatform != collision.gameObject)
            {
                score = score + 7;
                scoreText.text = score.ToString();
            }

            // Обновляем последнюю платформу
            lastPlatform = collision.gameObject;
        }
    }
    public int GetScore()
    {
        return score;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Death.instance.EndGame();
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        //Debug.Log("drawnPlatformAngle: " + drawnPlatformAngle);

        Vector2 platformDirection = GetPlatformNormal();
        ApplyJumpForce(platformDirection);
    }
    private void ApplyJumpForce(Vector2 normal)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            // Определяем вектор от центра до точки столкновения
            Vector2 collisionPoint = hit.point - (Vector2)transform.position;

            float currentRotation = transform.eulerAngles.z;

            // Корректируем угол прыжка
            float correctedAngle = Mathf.Atan2(collisionPoint.y, collisionPoint.x) * Mathf.Rad2Deg;

            // Добавляем угол коррекции к текущему углу поворота
            float newRotation = currentRotation + (correctedAngle - 90f);

            // Устанавливаем новый поворот персонажа
            transform.rotation = Quaternion.Euler(0, 0, newRotation);
        }
    }

    private Vector2 GetPlatformNormal()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            return hit.normal.normalized;
        }

        return Vector2.up; 
    }
    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartTime = Time.time;
                isDrawingPlatform = true;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                float touchDuration = Time.time - touchStartTime;
                isDrawingPlatform = false;

                // Проверка на стрельбу
                if (touchDuration < minTouchDuration)
                {
                    if (!isDrawingPlatform && PlatformDraw.instance.GetPlatformPointsCount() < 2)
                    {
                        Bullets.instance.Shoot();
                    }
                }
            }
        }
    }
}