using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Enemy.sfx += explosion;
    }

    private void OnDisable()
    {
        Enemy.sfx -= explosion;
    }

    public void explosion()
    {
        audio.Play();
    }
}
