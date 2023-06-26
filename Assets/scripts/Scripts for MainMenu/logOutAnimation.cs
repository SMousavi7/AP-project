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
        File.Delete("Difficulty.txt");
        StartCoroutine(ls(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator ls(int index)
    {
        animator.SetTrigger("end");
        Debug.Log("miad inja va 2 sanie sabr mikone");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(index);
    }
}
