using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private GameObject on;
    [SerializeField] private GameObject off;
    [SerializeField] private Slider slider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("volume"))
        {
            LoadValume();
        }
        else
        {
            setVolume();
        }
    }

    public void setVolume()
    {
        float volume = slider.value;
        //myMixer.SetFloat("vol", volume);
        //PlayerPrefs.SetFloat("volume", volume);
        AudioListener.volume = (volume + 60) / 68;
    }

    public void On()
    {
        AudioListener.volume = 1;
        on.SetActive(true);
    }

    public void Off()
    {
        AudioListener.volume = 0;
        off.SetActive(true);
    }

    private void LoadValume()
    {
        slider.value = PlayerPrefs.GetFloat("volume");
        setVolume();
    }
}
