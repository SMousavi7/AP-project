using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using static UnityEditor.ShaderData;

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
   
    string deletEnter(string str)
    {
        string res = "";
        for(int i =0; i < str.Length; i++)
        {
            if (str[i] == 8203)
            {
                continue;
            }
            res += str[i];
        }
        return res;
    }

    public void SignUp()
    {
        strPassword = inputFieldForPassword.GetComponent<TextMeshProUGUI>().text;
        strUserName = inputFieldForUsername.GetComponent<TextMeshProUGUI>().text;
        strPassword = deletEnter(strPassword);
        strUserName = deletEnter(strUserName);
       
        IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(serverAddress);

        // Sending
        string select = "1";
        byte[] temp = System.Text.Encoding.UTF8.GetBytes(select);
        clientSocket.Send(temp);
        int toSendLen = System.Text.Encoding.ASCII.GetByteCount(strUserName);
        byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(strUserName);
        byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
        clientSocket.Send(toSendLenBytes);
        clientSocket.Send(toSendBytes);

        int toSendLe = System.Text.Encoding.ASCII.GetByteCount(strPassword);
        byte[] toSendByte = System.Text.Encoding.ASCII.GetBytes(strPassword);
        byte[] toSendLenByte = System.BitConverter.GetBytes(toSendLe);
        clientSocket.Send(toSendLenByte);
        clientSocket.Send(toSendByte);

        byte[] rcvLenBytes = new byte[4];
        clientSocket.Receive(rcvLenBytes);
        int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
        byte[] rcvBytes = new byte[rcvLen];
        clientSocket.Receive(rcvBytes);
        String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);

        outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = rcv;
        if(outputFieldForUsername.GetComponent<TextMeshProUGUI>().text.Equals("welcome to the game " + strUserName))
        {
            LoadNextScene();
        }
        else
        {
            user.text = "";
            password.text = "";
            StartCoroutine(delay());
        }
    }

public void LogIn()
    {
        strPassword = inputFieldForPassword.GetComponent<TextMeshProUGUI>().text;
        strUserName = inputFieldForUsername.GetComponent<TextMeshProUGUI>().text;
        strPassword = deletEnter(strPassword);
        strUserName = deletEnter(strUserName);

        IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(serverAddress);

        // Sending
        string select = "2";
        byte[] temp = System.Text.Encoding.UTF8.GetBytes(select);
        clientSocket.Send(temp);
        int toSendLen = System.Text.Encoding.ASCII.GetByteCount(strUserName);
        byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(strUserName);
        byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
        clientSocket.Send(toSendLenBytes);
        clientSocket.Send(toSendBytes);

        int toSendLe = System.Text.Encoding.ASCII.GetByteCount(strPassword);
        byte[] toSendByte = System.Text.Encoding.ASCII.GetBytes(strPassword);
        byte[] toSendLenByte = System.BitConverter.GetBytes(toSendLe);
        clientSocket.Send(toSendLenByte);
        clientSocket.Send(toSendByte);

        byte[] rcvLenBytes = new byte[4];
        clientSocket.Receive(rcvLenBytes);
        int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
        byte[] rcvBytes = new byte[rcvLen];
        clientSocket.Receive(rcvBytes);
        String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);
        outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = rcv;
        if(outputFieldForUsername.GetComponent<TextMeshProUGUI>().text.Equals("you loged in successfully..."))
        {
            LoadNextScene();
        }
        else
        {
            user.text = "";
            password.text = "";
            StartCoroutine(delay());
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
