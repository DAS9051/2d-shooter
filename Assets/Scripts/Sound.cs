using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfx1;

    public void shootSound(){
        src.clip = sfx1;
        src.Play();
    }
}
