using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaygameAnimation : MonoBehaviour
{
    public Animator animator;
    public void LoadPreviousScene()
    {
        StartCoroutine(ls(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator ls(int index)
    {
        animator.SetTrigger("start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(index);
    }
}
