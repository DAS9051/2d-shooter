using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Public fields accessible from the Unity Editor
    public SmoothCamera smoothCamera; // Reference to the SmoothCamera script for camera control.
    public Transform target; // The current target of the enemy.
    public float speed = 3f; // Movement speed of the enemy.
    public float roateSpeed = 0.0025f; // Speed of rotation towards the target.

    private Rigidbody2D rb; // Private variable to hold the Rigidbody2D component.

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Getting the Rigidbody2D component from the GameObject.
    }

    // Update is called once per frame
    private void Update()
    {
        if (!target)
        {
            GetTarget(); // Acquire a target if none is currently set.
        }
        else
        {
            RotateTowardsTarget(); // Rotate towards the current target.
        }
    }

    // FixedUpdate is called at a fixed interval and is used for physics updates
    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed; // Moving the enemy forward at its set speed.
    }

    // Rotates the enemy towards its current target
    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position; // Direction vector towards the target.
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f; // Calculate the angle towards the target.
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle)); // Create a Quaternion rotation from the angle.
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, roateSpeed); // Smoothly rotate towards the target.
    }

    // Finds and sets the nearest player as the target
    private void GetTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // Find all GameObjects tagged as "Player".
        if (players.Length > 0)
        {
            target = players[0].transform; // Set the first found player as the target.
        }
    }

    // Called when this GameObject collides with another GameObject
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (smoothCamera != null)
            {
                smoothCamera.StopFollowing(); // Stops the camera from following if it's assigned.
            }
            LevelManager.manager.GameOver(); // Triggers GameOver in LevelManager.
            Destroy(collision.gameObject); // Destroys the player object on collision.
            target = null; // Clears the target reference.
        } 
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            LevelManager.manager.IncreaseScore(1); // Increase the score in LevelManager.
            Destroy(collision.gameObject); // Destroys the bullet object on collision.
            Destroy(gameObject); // Destroys this enemy object.
        }
    }
}
