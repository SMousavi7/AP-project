using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;
using TMPro;
using System.Net.Sockets;
using System.Net;
using System;

public class ChangeUsername : MonoBehaviour
{
    [SerializeField] private GameObject old_username;
    [SerializeField] private GameObject new_username;
    [SerializeField] private GameObject output;

    string delete(string str)
    {
        string res = "";
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == 8203)
            {
                continue;
            }
            res += str[i];
        }
        return res;
    }
    public void ChangeName()
    {

        string oldUse = old_username.GetComponent<TextMeshProUGUI>().text;
        string newUse = new_username.GetComponent<TextMeshProUGUI>().text;
        oldUse = delete(oldUse);
        newUse = delete(newUse);
        print(oldUse);
        print(newUse);
        if (oldUse.Equals(newUse))
        {
            output.GetComponent<TextMeshProUGUI>().text = "this username already exists...";
            StartCoroutine(delay());
        }
        else
        {
            IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(serverAddress);

            // Sending
            string select = "4";
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(select);
            clientSocket.Send(temp);
            int toSendLen = System.Text.Encoding.ASCII.GetByteCount(oldUse);
            byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(oldUse);
            byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
            clientSocket.Send(toSendLenBytes);
            clientSocket.Send(toSendBytes);


            int toSendLen1 = System.Text.Encoding.ASCII.GetByteCount(newUse);
            byte[] toSendBytes1 = System.Text.Encoding.ASCII.GetBytes(newUse);
            byte[] toSendLenBytes1 = System.BitConverter.GetBytes(toSendLen1);
            clientSocket.Send(toSendLenBytes1);
            clientSocket.Send(toSendBytes1);


            byte[] rcvLenBytes = new byte[4];
            clientSocket.Receive(rcvLenBytes);
            int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
            byte[] rcvBytes = new byte[rcvLen];
            clientSocket.Receive(rcvBytes);
            String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);
            print(rcv);
            if (rcv.Equals("true"))
            {
                output.GetComponent<TextMeshProUGUI>().text = "you changed your name successfully";
                StartCoroutine(delay());
                File.Delete("temp_username.txt");
                StreamWriter sw = new StreamWriter("temp_username.txt");
                sw.WriteLine(newUse); 
                sw.Close();
            }
            else if(rcv.Equals("false"))
            {
                output.GetComponent<TextMeshProUGUI>().text = "this username already exists...";
                StartCoroutine(delay());
            }
            else
            {
                output.GetComponent<TextMeshProUGUI>().text = "wrong username entered...";
                StartCoroutine(delay());
            }
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(3);
        output.GetComponent<TextMeshProUGUI>().text = "";
    }
}
