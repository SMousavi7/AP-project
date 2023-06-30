using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAnimation : MonoBehaviour
{
    public Animator animator;
    public void LoadNextScene()
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
