using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Echo : MonoBehaviour
{
    Socket socket;
    public string sendStr = "Test,test,test";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("TSt");
            Connection();
        }else if (Input.GetKeyDown(KeyCode.B))
        {
            Send();
        }
    }

    public void Connection()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //socket.Connect("127.0.0.1", 8888);
        socket.BeginConnect("127.0.0.1", 8888,connectCallback,socket);
    }

    public void connectCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket =(Socket)ar.AsyncState;
            socket.EndConnect(ar);
            Debug.Log("Socket Connect Succ");
        }
        catch(SocketException ex){
            Debug.Log("Socket Connect fail" + ex.ToString());
        }
        }

    public void Send()
    {
        byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
        //socket.Send(sendBytes);
        //byte[] readBuff = new byte[1024];
        //int count = socket.Receive(readBuff);
        //string recvStr = System.Text.Encoding.Default.GetString(readBuff, 0, count);
        //Debug.Log("Receive: " + recvStr);
        //socket.Close();

        socket.BeginSend(sendBytes, 0, sendBytes.Length, 0, sendCallback, socket);

    }

    public void sendCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            int count = socket.EndSend(ar);
            Debug.Log("Socket Send succ" + count);

        }
        catch(SocketException e)
        {
            Debug.Log("Socket Send fail" + e.ToString());
        }
    }


}
