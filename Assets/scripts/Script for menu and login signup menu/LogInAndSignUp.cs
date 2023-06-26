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
    public TMP_InputField user;
    public TMP_InputField password;
    public Animator animator;
    private object inputFieldForUserName;

    public void SignUp()
    {
        strPassword = inputFieldForPassword.GetComponent<TextMeshProUGUI>().text;
        strUserName = inputFieldForUsername.GetComponent<TextMeshProUGUI>().text;
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
                    user.text = "";
                    password.text = "";
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
                swWrite.Close();
                StreamWriter temp = new StreamWriter("temp_username.txt");
                temp.Write(strUserName);
                temp.Close();
                LoadNextScene();
            }
            StartCoroutine(delay());
        }
        else
        {
            StreamWriter swWrite = new StreamWriter("User.txt");
            outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = "dear " + strUserName + " you successfully signed up. welcom to the game...";
            swWrite.Write(strUserName + " " + strPassword + " " + "0\n");
            swWrite.Close();
            StreamWriter temp = new StreamWriter("temp_username.txt");
            temp.Write(strUserName);
            temp.Close();
            temp = new StreamWriter("Difficulty.txt");
            temp.Write(2);
            temp.Close();
            LoadNextScene();
        }

    }

    public void LogIn()
    {
        strPassword = inputFieldForPassword.GetComponent<TextMeshProUGUI>().text;
        strUserName = inputFieldForUsername.GetComponent<TextMeshProUGUI>().text;

        if(!File.Exists("User.txt")) {
            outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = "there is no account yet!!! please sign up first";
            user.text = "";
            password.text = "";
            StartCoroutine(delay());
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
                    StreamWriter temp = new StreamWriter("temp_username.txt");
                    temp.Write(strUserName);
                    temp.Close();
                    temp = new StreamWriter("Difficulty.txt");
                    temp.Write(2);
                    temp.Close();
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
                user.text = "";
                password.text = "";
                srReader.Close();
                StartCoroutine(delay());
            }
        }
    }

    public void ChangePassword()
    {
        strPassword = inputFieldForPassword.GetComponent<TextMeshProUGUI>().text;
        strUserName = inputFieldForUsername.GetComponent<TextMeshProUGUI>().text;

        int index = 0;
        if (!File.Exists("User.txt"))
        {
            outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = "there is no account yet!!! please sign up first";
            user.text = "";
            password.text = "";
            StartCoroutine(delay());
        }
        else
        {
            StreamReader reader = new StreamReader("User.txt");
            int i = 1;
            bool flag = false;
            while(!reader.EndOfStream)
            {
                string str = reader.ReadLine();
                string[] splited = str.Split(" ");
                if (splited[0].EndsWith(strUserName))
                {
                    flag = true;
                    index = i;
                    break;
                }
                i++;
            }
            if(!flag)
            {
                outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = "username you entered is not exists";
                reader.Close();
                user.text = "";
                password.text = "";
                StartCoroutine(delay());
            }
            else
            {
                StreamWriter writer = new StreamWriter("newUser.txt");
                reader.Close();
                reader = new StreamReader("User.txt");
                int j = 1;
                while(!reader.EndOfStream)
                {
                    string str = reader.ReadLine();
                    if (index == j)
                    {
                        string[] splited = str.Split(" ");
                        splited[1] = strPassword;
                        str = splited[0] + " " + splited[1] + " " + splited[2] + "\n";
                    }
                    writer.Write(str);
                    writer.Flush();
                    j++;
                }
                reader.Close();
                writer.Close();
                File.Delete("User.txt");
                File.Move("newUser.txt", "User.txt");
                outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = strUserName+" you changed your password successfully";
                StartCoroutine(delay());
                
            }


        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(ls(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator ls(int index)
    {
        animator.SetTrigger("Start");
        Debug.Log("miad inja va 2 sanie sabr mikone");
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(index);
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(3);
        outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = "";
    }
}
