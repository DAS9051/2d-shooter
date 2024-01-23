using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull : MonoBehaviour
{
    // SerializeField allows these private fields to be set in the Unity Editor.
    // Range attribute restricts the values of speed and lifetime between 1 and 10.
    [Range(1, 10)]
    [SerializeField] private float speed = 10f; // The speed at which the bull moves.

    [Range(1, 10)]
    [SerializeField] private float lifetime = 3f; // The lifetime of the bull object in seconds.

    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the same GameObject.

    // Start is called before the first frame update.
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Getting the Rigidbody2D component.
        Destroy(gameObject, lifetime); // Destroy the GameObject after 'lifetime' seconds.
    }

    // FixedUpdate is called once per physics frame.
    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed; // Moving the bull in the 'up' direction relative to its current rotation, multiplied by speed.
    }

    // Called when this GameObject collides with another GameObject.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Check if the collided object has the tag 'Player'.
        {
            LevelManager.manager.GameOver(); // Call the GameOver function on the LevelManager.
            Destroy(collision.gameObject); // Destroy the collided player GameObject.
        }
    }
}
