using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : MonoBehaviour
{
    public SmoothCamera smoothCamera;

    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.25f;
    private Rigidbody2D rb;

    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;

    public float fireRate;
    private float timeToFire;

    public Transform firingPoint;
    public GameObject bulletPrefab; // Assuming you have a bullet prefab

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!target)
        {
            GetTarget();
            return; // Skip the rest if no target
        }

        RotateTowardsTarget();

        if (Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (target != null && Vector2.Distance(target.position, transform.position) > distanceToStop)
        {
            rb.velocity = transform.up * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation); // Shoot a bullet
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, q, rotateSpeed);
    }

    private void GetTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform closestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (var player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < minDistance)
            {
                closestTarget = player.transform;
                minDistance = distance;
            }
        }

        target = closestTarget;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (smoothCamera != null)
            {
                smoothCamera.StopFollowing();
            }
            LevelManager.manager.GameOver();
            Destroy(collision.gameObject);
            target = null;
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            LevelManager.manager.IncreaseScore(3);
            Destroy(collision.gameObject);
            Destroy(gameObject); // Consider adding a health system instead
        }
    }
}
