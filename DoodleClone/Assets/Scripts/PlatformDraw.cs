using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDraw : MonoBehaviour
{
    public static PlatformDraw instance;
    public LineRenderer lineRenderer;
    public GameObject platformPrefab; // Префаб платформы
    private List<Vector2> platformPoints = new List<Vector2>();
    private float drawnPlatformAngle;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                platformPoints.Add(touchPosition);
                lineRenderer.positionCount = platformPoints.Count;
                lineRenderer.SetPosition(platformPoints.Count - 1, touchPosition);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Когда касание завершается, создаем платформу и сбрасываем линию
                CreatePlatform();
                ResetLine();
            }
        }
    }

    void CreatePlatform()
    {
        if (platformPoints.Count >= 2)
        {
            // Создаем платформу по точкам, где была проведена линия
            GameObject platform = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);
            CapsuleCollider2D platformCollider = platform.GetComponent<CapsuleCollider2D>();

            // Находим центр и длину платформы по точкам
            Vector2 center = (platformPoints[0] + platformPoints[platformPoints.Count - 1]) / 2f;
            float length = Vector2.Distance(platformPoints[0], platformPoints[platformPoints.Count - 1]);

            // Определяем угол между точками и вращаем платформу
            float angle = Mathf.Atan2(platformPoints[platformPoints.Count - 1].y - platformPoints[0].y,
                                      platformPoints[platformPoints.Count - 1].x - platformPoints[0].x) * Mathf.Rad2Deg;
            drawnPlatformAngle = angle;
            platform.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Устанавливаем параметры CapsuleCollider2D
            platformCollider.size = new Vector2(platformCollider.size.x, platformCollider.size.y);

            // Устанавливаем позицию коллайдера с учетом центра платформы и ее вращения
            platformCollider.transform.position = center;
            platformCollider.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Передаем ближайшую платформу Doodle
            //Doodle.instance.SetNearestPlatform(platform);

            // Очищаем точки
            platformPoints.Clear();
        }
        else
        {
            Debug.Log("ПИУ ПИУ");
            Bullets.instance.Shoot();
        }
    }

    void ResetLine()
    {
        // Сбрасываем линию
        lineRenderer.positionCount = 0;
    }
    public int GetPlatformPointsCount()
    {
        return platformPoints.Count;
    }
}
