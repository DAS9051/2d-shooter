using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource src; // Reference to the AudioSource component
    public AudioClip sfx1; // The shooting sound effect

    // Method to play the shooting sound
    public void shootSound()
    {
        src.clip = sfx1; // Set the AudioSource's clip to the shooting sound effect
        src.Play(); // Play the sound effect
    }
}
