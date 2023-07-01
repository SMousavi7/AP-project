using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSound : MonoBehaviour
{
    AudioSource audio;
    // Start is called before the first frame update
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void play()
    {
        audio.Play();
    }
    
}
