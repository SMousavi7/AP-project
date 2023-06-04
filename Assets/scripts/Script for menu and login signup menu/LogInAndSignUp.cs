using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class LogInAndSignUp : MonoBehaviour
{
    string strPassword;
    string strUserName;
    public GameObject inputFieldForPassword;
    public GameObject inputFieldForUsername;
    public GameObject outputFieldForUsername;
    public Animator animator;
    
    public void SignUp()
    {
        strPassword = inputFieldForPassword.GetComponent<TextMeshProUGUI>().text;
        strUserName = inputFieldForUsername.GetComponent<TextMeshProUGUI>().text;
        //Debug.Log(s.Length);
        //Debug.Log(strUserName);
        if(File.Exists("User.txt")) {
            StreamReader srUser = new StreamReader("User.txt");
            bool flag = true;
            while( !srUser.EndOfStream )
            {    
                string fullLine = srUser.ReadLine();
                string[] tempUser = fullLine.Split(" ");
                if (tempUser[0].CompareTo(strUserName) == 0)
                {
                    outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = strUserName + " already exists!!!";
                    flag = false;
                    break;
                }
            }
            srUser.Close();
            if(flag)
            {
                outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = "dear " + strUserName + " you succefully signed up. welcom to the game...";
                StreamWriter swWrite = File.AppendText("User.txt");
                swWrite.Write(strUserName + " " + strPassword + " " + "0\n");
                LoadNextScene();
                swWrite.Close();
            }
        }
        else
        {
            StreamWriter swWrite = new StreamWriter("User.txt");
            outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = "dear " + strUserName + " you successfully signed up. welcom to the game...";
            swWrite.Write(strUserName + " " + strPassword + " " + "0\n");
            LoadNextScene();
            swWrite.Close();
        }

    }

    public void LogIn()
    {
        strPassword = inputFieldForPassword.GetComponent<TextMeshProUGUI>().text;
        strUserName = inputFieldForUsername.GetComponent<TextMeshProUGUI>().text;

        if(!File.Exists("User.txt")) {
            outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = "there is no account yet!!! please sign up first";
        }
        else
        {
            StreamReader srReader = new StreamReader("User.txt");
            bool flag = false;
            while(!srReader.EndOfStream)
            {

                string fullLine = srReader.ReadLine();
                string[] tempUser = fullLine.Split(" ");
                if (tempUser[0].Equals(strUserName) && tempUser[1].Equals(strPassword))
                {
                    outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = "you loged in successfully...";
                    srReader.Close();
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                LoadNextScene();
            }else
            {
                outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = "username or password is not correct...";
            }
        }
    }

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
