using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    public Animator animator;
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private GameObject on;
    [SerializeField] private GameObject off;
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource audio;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            audio.Play();
            if(GameIsPaused)
            {
                resuem();
            }
            else
            {
                pause();
            }
        }
    }

    public void resuem()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void retry()
    {
        LoadThisScene();
    }
    void LoadThisScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadPreviousScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator LoadScene(int index)
    {
        animator.SetTrigger("start");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(index);
    }



    private void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
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
        myMixer.SetFloat("vol", volume);
        PlayerPrefs.SetFloat("volume", volume);
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
