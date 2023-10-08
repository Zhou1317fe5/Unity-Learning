using UnityEngine;
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

// UDP_Arm2D2018

public class ArmController : MonoBehaviour
{


    /*简易机械臂*/
    public float gravity = 9.8f; // 重力加速度，单位：米/秒²
    public float armMass = 15f; // 机械臂质量，单位：千克
    public float armLength = 0.5f; // 机械臂长度，单位：米
    public float frictionCoefficient = 0.1f; // 机械臂转轴处摩擦系数

    private float armInertia; // 机械臂的转动惯量
    private Vector2 armState = Vector2.zero; // 机械臂的初始状态向量
    private Transform armTransform; // 机械臂的Transform组件
    //private Rigidbody2D armRigidbody; // 机械臂的Rigidbody2D组件
    float forceMagnitude;/* 获取输入的力的大小 */
    public Button button;
    public Button button1;

    public UDPServer SendMess;

    void Start()
    {

        
      
        Debug.Log("简易机械臂初始化开始");
        /*简易机械臂*/
        armTransform = GetComponent<Transform>();
        //armRigidbody = GetComponent<Rigidbody2D>();

        //button1 = GameObject.Find("Button1").GetComponent<Button>();
        //button = GameObject.Find("Button").GetComponent<Button>();
        armInertia = armMass * armLength * armLength / 3f;//计算转动惯量

        //button.onClick.AddListener(THForce);
        //button1.onClick.AddListener(NTHForce);
        // forceMagnitudeInput = GameObject.Find("Canvas").GetComponent<InputField>();//初始化对象
        Debug.Log("简易机械臂初始化完成");
        
    }
    

    void Update()
    {

        /*简易机械臂*/
        // 计算速度
        float angularAcceleration = forceMagnitude / armInertia;
        float angularVelocity = armState.y + angularAcceleration * Time.deltaTime; // 进行数值积分

        // 将速度应用于机械臂
        armState.y = angularVelocity;
        armTransform.eulerAngles = new Vector3(0f, 0f, armState.y); // 根据速度更新机械臂的旋转角度
        //armRigidbody.angularVelocity = angularVelocity; // 应用角速度到Rigidbody组件
        // this.forceMagnitude = this.forceMagnitude * 0.9999f;

        //关于摩擦系数和时间帧率的速度衰减函数
        this.forceMagnitude *= (1f - frictionCoefficient * Time.deltaTime);



    }

    public void THForce()  // 点击调用
    {
        forceMagnitude = 200f;
        // 调用UDPServer的SocketSendDefault方法
        SendMess.SocketSendDefault("200");
    }
    public void NTHForce()
    {
        forceMagnitude = -200f;
        // 调用UDPServer的SocketSendDefault方法
        SendMess.SocketSendDefault("-200");
    }
}