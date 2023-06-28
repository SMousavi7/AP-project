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
    private string ipAddress = "127.0.0.1";
    private int port = 999;

    public void SignUp()
    {
        strPassword = inputFieldForPassword.GetComponent<TextMeshProUGUI>().text;
        strUserName = inputFieldForUsername.GetComponent<TextMeshProUGUI>().text;
        /*if(File.Exists("User.txt")) {
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
        }*/
            try
            {
                // Establish the remote endpoint for the socket.
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 12345;
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                Socket clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                clientSocket.Connect(remoteEP);

                Console.WriteLine("Connected to the server!");

                // Send data to the server.
                string message = "Hello from the client!";
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                clientSocket.Send(buffer);

                // Receive data from the server.
                byte[] receivedBuffer = new byte[1024];
                int bytesRead = clientSocket.Receive(receivedBuffer);
                string receivedMessage = Encoding.ASCII.GetString(receivedBuffer, 0, bytesRead);
                Console.WriteLine("Received from server: " + receivedMessage);

                // Close the socket.
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
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
