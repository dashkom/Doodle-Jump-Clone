using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public static Bullets instance;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        // ��������� ����� ���� �� ������� ������ � ���������� ��
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            if (!IsBulletWithinScreenBounds(bullet))
            {
                Destroy(bullet);
            }
        }
    }
    public void Shoot()
    {
        // ���������� ���������� ����������
        GameObject closestEnemy = FindClosestEnemy();

        if (IsEnemyWithinScreenBounds(closestEnemy))
        {
            Vector2 shootDirection = closestEnemy.transform.position - firePoint.position;
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            // ������� ����
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));

            // ��������� ��������� Rigidbody2D � ������������� ��������� �������� � ����������� shootDirection
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = shootDirection.normalized * bulletSpeed;

            // ��������� ��������� Collider2D � ����
            Collider2D bulletCollider = bullet.AddComponent<BoxCollider2D>();
            bulletCollider.isTrigger = true;
        }
        // ���� ��������� ����������� ��� � �������� ������ ������, �������� �����
        else
        {
            Vector2 shootDirection = Vector2.up;
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            // ������� ����
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));

            // ��������� ��������� Rigidbody2D � ������������� ��������� �������� �����
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = shootDirection.normalized * bulletSpeed;

            // ��������� ��������� Collider2D � ����
            Collider2D bulletCollider = bullet.AddComponent<BoxCollider2D>();
            bulletCollider.isTrigger = true;
        }
    }
    private bool IsBulletWithinScreenBounds(GameObject bullet)
    {
        if (bullet == null)
            return false;

        Camera camera = Camera.main;
        Vector3 bulletScreenPoint = camera.WorldToScreenPoint(bullet.transform.position);

        // ���������, ��������� �� ���� ������ ������
        return bulletScreenPoint.x >= 0 && bulletScreenPoint.x <= Screen.width &&
               bulletScreenPoint.y >= 0 && bulletScreenPoint.y <= Screen.height;
    }
    private bool IsEnemyWithinScreenBounds(GameObject enemy)
    {
        if (enemy == null)
            return false;

        Camera camera = Camera.main;
        Vector3 enemyScreenPoint = camera.WorldToScreenPoint(enemy.transform.position);

        // ���������, ��������� �� ��������� ������ ������
        return enemyScreenPoint.x >= 0 && enemyScreenPoint.x <= Screen.width &&
               enemyScreenPoint.y >= 0 && enemyScreenPoint.y <= Screen.height;
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }
}
