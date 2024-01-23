using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Speed of the player
    // Serialized fields for bullet properties
    [SerializeField] private GameObject bulletPrefab; // Prefab of the bullet to shoot
    [SerializeField] private Transform firingPoint; // Transform from where the bullet is fired
    [Range(0.1f, 2f)] // Restricts the fireRate value between 0.1 and 2 in the Unity Inspector
    [SerializeField] private float fireRate = 0.5f; // Rate at which the player can fire

    private Rigidbody2D rb; // Rigidbody component for physics
    private float mx; // Horizontal input
    private float my; // Vertical input

    private float fireTimer; // Timer to control firing rate

    private Vector2 mousePos; // Position of the mouse

    public Sound soundScript; // Reference to the Sound script for sound effects

    // Start is called before the first frame update
    private void Start()
    {
        // Getting the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Getting input from the player
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");

        // Converting mouse position to world coordinates
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculating the angle to face the mouse pointer
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        // Rotating the player to face the mouse pointer
        transform.localRotation = Quaternion.Euler(0, 0, angle);

        // Handling shooting
        if (Input.GetMouseButton(0) && fireTimer <= 0f) // Check if left mouse button is pressed and fireTimer is elapsed
        {
            Shoot();
            fireTimer = fireRate; // Resetting fireTimer
        }
        else
        {
            fireTimer -= Time.deltaTime; // Decrease fireTimer
        }
    }

    private void FixedUpdate()
    {
        // Moving the player based on input
        rb.velocity = new Vector2(mx, my).normalized * speed;
    }

    private void Shoot()
    {
        // Creating a bullet instance at the firing point with the correct rotation
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);

        // Playing the shooting sound
        soundScript.shootSound();

        // Resetting fireTimer to 0
        fireTimer = 0;
    }
}
