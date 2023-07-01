using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Animator animator;
    [SerializeField] GameObject on;
    [SerializeField] GameObject off;
    [SerializeField] GameObject Setting;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
        
    }

    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void loadMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void retry()
    {
        SceneManager.LoadScene("Game");
    }

    public void setting()
    {
        Setting.SetActive(true);
    }
    public void quit()
    {
        print("game closed");
        Application.Quit();
    }
    public void LoadPreviousScene()
    {
        StartCoroutine(ls(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator ls(int index)
    {

        SceneManager.LoadScene(index - 1);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(index);
        animator.SetTrigger("start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(index);
    }

    public void LoadThisScene()
    {
        StartCoroutine(ls(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator newls(int index)
    {
        SceneManager.LoadScene(index - 1);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(index);
        animator.SetTrigger("start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(index);
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
}
