using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class logOutAnimation : MonoBehaviour
{
    public Animator animator;
    public void LoadPreviousScene()
    {
        File.Delete("temp_username.txt");
        StartCoroutine(ls(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator ls(int index)
    {
        animator.SetTrigger("Start");
        Debug.Log("miad inja va 2 sanie sabr mikone");
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(index);
    }
}
