using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioSource ButtonSound;

    public void PlaySoundEffect()
    {
        ButtonSound.Play();
    }
}
