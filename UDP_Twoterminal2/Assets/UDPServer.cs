﻿using UnityEngine;
using System.Collections;
//引入库
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine.UI;

//https://www.bilibili.com/video/BV18f4y1F7zq/?spm_id_from=333.880.my_history.page.click&vd_source=17734ded95832985fab6fd54148bc78d

public class UDPServer : MonoBehaviour
{


    private bool getNew = false;


    //输入框
    public InputField sendMessage;
    //显示接收到的文本
    public Text displayText;
    //显示本机IP
    public Text displayselfAddress;
    // 获取用户在输入框中输入的oppositeIP
    public InputField InputoppositeIP;
    // 获取用户在输入框中输入的oppositePort
    public InputField InputoppositePort;
    // 获取用户在输入框中输入的selfConnectPoint
    public InputField InputselfConnectPoint;




    //定义客户端通信插口
    Socket socket_c;
    [Header("---------------------客户端--------------------")]
    public string biji = "可以在这里写笔记";
    [Header("存放对方的接收ip")]
    public string oppositeIP = "192.168.43.5";
    [Header("存放对方的接收端口")]
    public int oppositePort = 8888;
    //对方服务端ip端口对
    IPEndPoint oppositeIpEnd;
    //广播的所有ip
    List<string> allIPv4 = new List<string>();



    //定义服务端通信插口
    Socket socket_s;
    [Header("---------------------服务端--------------------")]

    [Header("本机开放的端口")]
    public int selfConnectPoint = 7777;
    // [Header("本机ip自动获取")]
    public static string selfAddress = "";
    //本机服务端所监听的ip端口对
    EndPoint selfListenEnd;
    //监听线程
    Thread connectThread;

    [Header("------------------入站的字符串---------------")]
    public string recvStr;


    //定义接受byte数组的长度
    int recvLen = 0;
    //接收到的byte数据
    byte[] recvData = new byte[1024];
    //要发送的的byte数据
    byte[] sendData = new byte[1024];


    void Start()
    {

        // 将 oppositeIP 的初始值赋值给 InputField 组件(inputoppositeIP)
        InputoppositeIP.text = oppositeIP;
        // 将 oppositePort 的初始值赋值给 InputField 组件(InputoppositePort)
        InputoppositePort.text = oppositePort.ToString();
        // 将 selfConnectPoint 的初始值赋值给 InputField 组件(InputselfConnectPoint)
        InputselfConnectPoint.text= selfConnectPoint.ToString();


        //去除ip 前后空格
        oppositeIP = oppositeIP.Trim();
        //定义本机服务器的ip和端口对
        if (selfAddress == "")
        {
            selfAddress = GetLocalIP();
        }
        selfAddress = selfAddress.Trim();
        GetBroadcastIP(selfAddress);
        //初始化
        InitSocket();

        // 显示本机IP
        displayselfAddress.text = selfAddress;
    }
    void InitSocket()
    {
        //作为客户端
        //定义对方的ip和端口对
        oppositeIpEnd = new IPEndPoint(IPAddress.Parse(oppositeIP), oppositePort);
        socket_c = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        //作为服务端    
        //本机服务器监听的ip端口对
        selfListenEnd = new IPEndPoint(IPAddress.Parse(selfAddress), selfConnectPoint);
        socket_s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket_s.Bind(selfListenEnd);
        //开启本机服务器监听线程
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();

        //给目标服务端发送数据测试  
        SocketSendDefault(selfAddress.ToString() + ":connected");
        //SocketSendDefault(selfListenEnd.ToString() + ":first");
        print("对方接到表示连接成功");
    }


    //本机服务器监听
    void SocketReceive()
    {
        while (true)
        {
            recvData = new byte[1024];
            recvLen = socket_s.ReceiveFrom(recvData, ref selfListenEnd);
            recvStr = Encoding.UTF8.GetString(recvData, 0, recvLen);
            Debug.Log("信息来自" + selfListenEnd.ToString() + ":" + recvStr);
            getNew = true;
        }
    }

    //默认ip端口发送
    public void SocketSendDefault(string sendStr)
    {
        //清空
        sendData = new byte[1024];
        //数据转换
        sendData = Encoding.UTF8.GetBytes(sendStr);
        //发送给指定服务端
        socket_c.SendTo(sendData, sendData.Length, SocketFlags.None, oppositeIpEnd);
    }
    public void ToSendString()
    {
        if (sendMessage.text != "")
        {
            SocketSendDefault(sendMessage.text);
        }
    }

    //自定义ip端口发送
    public void SocketSendCustom(string sendStr, string ip, int port)
    {
        //清空
        sendData = new byte[1024];
        //数据转换
        sendData = Encoding.UTF8.GetBytes(sendStr);
        IPEndPoint aimIpEnd;

        aimIpEnd = new IPEndPoint(IPAddress.Parse(ip), port);
        //发送给指定服务端
        socket_c.SendTo(sendData, sendData.Length, SocketFlags.None, aimIpEnd);
    }

    //默认ip端口群发
    public void SendToAllDefault(string sendMsg)
    {
        StartCoroutine(SocketBroadcastSend(sendMsg, oppositePort));
    }

    //自定义ip端口群发
    public void SendToAllCustom(string sendMsg, int port)
    {
        StartCoroutine(SocketBroadcastSend(sendMsg, port));

    }
    IEnumerator SocketBroadcastSend(string sendStr, int port)
    {
        //清空
        byte[] sendData_thread = new byte[1024];
        //数据转换
        sendData_thread = Encoding.UTF8.GetBytes(sendStr);
        IPEndPoint aimIpEnd;

        foreach (string ip in allIPv4)
        {
            Debug.Log(ip);
            aimIpEnd = new IPEndPoint(IPAddress.Parse(ip), port);

            socket_c.SendTo(sendData_thread, sendData_thread.Length, SocketFlags.None, aimIpEnd);
        }
        yield return null;
    }


    //连接关闭
    void SocketQuit()
    {
        //关闭线程
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最后关闭socket
        if (socket_c != null)
            socket_c.Close();
        if (socket_s != null)
            socket_s.Close();

        StopAllCoroutines();
    }
    void OnApplicationQuit()
    {
        SocketQuit();
    }

    void Update()
    {
        if (getNew == true)
        {
            getNew = false;
            //执行接收文本事件
            displayText.text = recvStr;
        }
    }

    // 更新IP和端口，重新初始化
    public void reInit()
    {
        //客户端
        oppositeIP = InputoppositeIP.text;
        oppositePort = int.Parse(InputoppositePort.text);

        //服务端
        selfConnectPoint = int.Parse(InputselfConnectPoint.text);
        //int newOppositePort = int.Parse(InputoppositePort.text);
        //oppositePort = newOppositePort;

        //关闭连接
        SocketQuit();
        // 重新初始化
        InitSocket();

    }


    public static string GetLocalIP()
    {
        try
        {
            string HostName = Dns.GetHostName(); //得到主机名
            IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
            for (int i = 0; i < IpEntry.AddressList.Length; i++)
            {
                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    Debug.Log(IpEntry.AddressList[i].ToString());
                    return IpEntry.AddressList[i].ToString();
                }
            }
            return "noIp";
        }
        catch
        {
            Debug.Log("ipGetFailed");
            return ("ipGetFailed");
        }
    }

    public void GetBroadcastIP(string ip)
    {
        string[] nums = ip.Split('.');
        string head = nums[0] + "." + nums[1] + "." + nums[2] + ".";
        for (int i = 1; i < 255; i++)
        {

            allIPv4.Add(head + i.ToString());
        }
    }

    //public static bool IsCorrectIP(string ip)
    //{
    //    return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
    //}

    [ContextMenu("测试单发效果")]
    private void SendToOne()
    {
        SocketSendDefault("单发测试成功");
    }
}