    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    public Animator animator;
    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadScene(int index)
    {
        animator.SetTrigger("start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }
}
