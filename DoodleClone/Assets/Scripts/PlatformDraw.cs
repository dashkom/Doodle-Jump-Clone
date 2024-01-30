using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDraw : MonoBehaviour
{
    public static PlatformDraw instance;
    public LineRenderer lineRenderer;
    public GameObject platformPrefab; // ������ ���������
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
                // ����� ������� �����������, ������� ��������� � ���������� �����
                CreatePlatform();
                ResetLine();
            }
        }
    }

    void CreatePlatform()
    {
        if (platformPoints.Count >= 2)
        {
            // ������� ��������� �� ������, ��� ���� ��������� �����
            GameObject platform = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);
            CapsuleCollider2D platformCollider = platform.GetComponent<CapsuleCollider2D>();

            // ������� ����� � ����� ��������� �� ������
            Vector2 center = (platformPoints[0] + platformPoints[platformPoints.Count - 1]) / 2f;
            float length = Vector2.Distance(platformPoints[0], platformPoints[platformPoints.Count - 1]);

            // ���������� ���� ����� ������� � ������� ���������
            float angle = Mathf.Atan2(platformPoints[platformPoints.Count - 1].y - platformPoints[0].y,
                                      platformPoints[platformPoints.Count - 1].x - platformPoints[0].x) * Mathf.Rad2Deg;
            drawnPlatformAngle = angle;
            platform.transform.rotation = Quaternion.Euler(0, 0, angle);

            // ������������� ��������� CapsuleCollider2D
            platformCollider.size = new Vector2(platformCollider.size.x, platformCollider.size.y);

            // ������������� ������� ���������� � ������ ������ ��������� � �� ��������
            platformCollider.transform.position = center;
            platformCollider.transform.rotation = Quaternion.Euler(0, 0, angle);

            // �������� ��������� ��������� Doodle
            //Doodle.instance.SetNearestPlatform(platform);

            // ������� �����
            platformPoints.Clear();
        }
        else
        {
            Debug.Log("��� ���");
            Bullets.instance.Shoot();
        }
    }

    void ResetLine()
    {
        // ���������� �����
        lineRenderer.positionCount = 0;
    }
    public int GetPlatformPointsCount()
    {
        return platformPoints.Count;
    }
}
