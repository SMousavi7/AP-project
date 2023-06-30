using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System;

public class LeaderBoard : MonoBehaviour
{

    public void leaderBoard()
    {
        IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(serverAddress);

        // Sending
        string select = "5";
        byte[] temp = System.Text.Encoding.UTF8.GetBytes(select);
        clientSocket.Send(temp);

        string rcv = "";
        ArrayList names = new ArrayList();
        ArrayList points = new ArrayList();
        while(!rcv.Equals("end"))
        {
            byte[] rcvLenBytes = new byte[4];
            clientSocket.Receive(rcvLenBytes);
            int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
            byte[] rcvBytes = new byte[rcvLen];
            clientSocket.Receive(rcvBytes);
            rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);
            if(rcv.Equals("end"))
            {
                break;
            }
            else
            {
                names.Add(rcv);
            }
            byte[] rcvLenByte = new byte[4];
            clientSocket.Receive(rcvLenByte);
            int rcvLen1 = System.BitConverter.ToInt32(rcvLenByte, 0);
            byte[] rcvByte = new byte[rcvLen1];
            clientSocket.Receive(rcvByte);
            rcv = System.Text.Encoding.ASCII.GetString(rcvByte);
            points.Add(rcv);

        }

        for(int i = 0; i < points.Count; i++)
        {
            for(int j = 0; j < points.Count - 1; j++)
            {
                if (Convert.ToInt32(points[j]) < Convert.ToInt32(points[j + 1]))
                {
                    string temp1 = (string)points[j];
                    points[j] = points[j + 1];
                    points[j + 1] = temp1;
                    string temp2 = (string)names[j];
                    names[j] = names[j + 1];
                    names[j + 1] = temp2;
                }
            }
        }

        
    }
}
