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
            StreamWriter writer = new StreamWriter("temp_username.txt");
            writer.Write(strUserName);
            writer.Close();
            writer = new StreamWriter("Difficulty.txt");
            writer.Write(2);
            writer.Close();
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
            StreamWriter writer = new StreamWriter("temp_username.txt");
            writer.Write(strUserName);
            writer.Close();
            writer = new StreamWriter("Difficulty.txt");
            writer.Write(2);
            writer.Close();
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

        strUserName = deletEnter(strUserName);
        strPassword = deletEnter(strPassword);


        IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(serverAddress);

        // Sending
        string select = "3";
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

        user.text = "";
        password.text = "";
        StartCoroutine(delay());
    }

    public void LoadNextScene()
    {
        StartCoroutine(ls(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator ls(int index)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(index);
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(3);
        outputFieldForUsername.GetComponent<TextMeshProUGUI>().text = "";
    }
}
