using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : MonoBehaviour
{
    public SmoothCamera smoothCamera; // Reference to a camera script for camera control

    public Transform target; // Current target of the ranged enemy
    public float speed = 3f; // Movement speed of the enemy
    public float rotateSpeed = 0.25f; // Speed at which the enemy rotates towards the target
    private Rigidbody2D rb; // Rigidbody component for physics

    public float distanceToShoot = 5f; // Distance within which the enemy starts shooting
    public float distanceToStop = 3f; // Distance at which the enemy stops moving towards the target

    public float fireRate; // Rate at which the enemy fires
    private float timeToFire; // Timer to control firing rate

    public Transform firingPoint; // Point from where the bullets are fired
    public GameObject bulletPrefab; // Prefab for the bullets that are shot

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Getting the Rigidbody2D component
    }

    private void Update()
    {
        if (!target)
        {
            GetTarget(); // Find a target if none is assigned
            return; // Skip the rest of the update if no target is found
        }

        RotateTowardsTarget(); // Rotate the enemy to face the target

        // Check if the target is within shooting range
        if (Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            Shoot(); // Shoot at the target
        }
    }

    private void FixedUpdate()
    {
        // Move towards the target if it is further than the stopping distance
        if (target != null && Vector2.Distance(target.position, transform.position) > distanceToStop)
        {
            rb.velocity = transform.up * speed; // Apply velocity towards the target
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop moving if within stopping distance
        }
    }

    private void Shoot()
    {
        if (timeToFire <= 0f) // Check if ready to fire
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation); // Create a bullet
            timeToFire = fireRate; // Reset the firing timer
        }
        else
        {
            timeToFire -= Time.deltaTime; // Decrease the firing timer
        }
    }

    private void RotateTowardsTarget()
    {
        // Calculate the direction to the target
        Vector2 targetDirection = target.position - transform.position;
        // Calculate the angle towards the target
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        // Create a rotation towards the target
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        // Rotate the enemy towards the target smoothly
        transform.rotation = Quaternion.Slerp(transform.rotation, q, rotateSpeed);
    }

    private void GetTarget()
    {
        // Find all game objects tagged as "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform closestTarget = null;
        float minDistance = Mathf.Infinity;

        // Loop through all players to find the closest one
        foreach (var player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < minDistance)
            {
                closestTarget = player.transform;
                minDistance = distance;
            }
        }

        target = closestTarget; // Assign the closest player as the target
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Logic for when colliding with a player
            if (smoothCamera != null)
            {
                smoothCamera.StopFollowing(); // Stop the camera from following
            }
            LevelManager.manager.GameOver(); // Trigger game over
            Destroy(collision.gameObject); // Destroy the player object
            target = null; // Clear the target
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            // Logic for when hit by a bullet
            LevelManager.manager.IncreaseScore(3); // Increase the player's score
            Destroy(collision.gameObject); // Destroy the bullet
            Destroy(gameObject); // Destroy the enemy (consider adding a health system)
        }
    }
}
