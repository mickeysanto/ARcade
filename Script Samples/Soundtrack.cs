using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    public AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        if(audio.clip != null )
        {
            audio.Play();
        }
    }
}
