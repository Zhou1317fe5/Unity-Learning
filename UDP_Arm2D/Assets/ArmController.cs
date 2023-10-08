using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmController : MonoBehaviour
{
    public float gravity = 9.8f; // 重力加速度，单位：米/秒²
    public float armMass = 15f; // 机械臂质量，单位：千克
    public float armLength = 0.5f; // 机械臂长度，单位：米
    public float frictionCoefficient = 0.1f; // 机械臂转轴处摩擦系数

    private float armInertia; // 机械臂的转动惯量
    private Vector2 armState = Vector2.zero; // 机械臂的初始状态向量
    private Transform armTransform; // 机械臂的Transform组件
    private Rigidbody2D armRigidbody; // 机械臂的Rigidbody2D组件

    // public UnityEngine.UI.InputField forceMagnitudeInput; //引入输入框的对象

    public Button button;
    public Button button1;



    float forceMagnitude;/* 获取输入的力的大小 */
    // Start is called before the first frame update
    void Start()
    {
        armTransform = GetComponent<Transform>();
        armRigidbody = GetComponent<Rigidbody2D>();

        button1 = GameObject.Find("Button1").GetComponent<Button>();
        button = GameObject.Find("Button").GetComponent<Button>();


        armInertia = armMass * armLength * armLength / 3f;//计算转动惯量
        
        button.onClick.AddListener(THForce);
        button1.onClick.AddListener(NTHForce);
        // forceMagnitudeInput = GameObject.Find("Canvas").GetComponent<InputField>();//初始化对象
    }

    
    public void THForce(){
        forceMagnitude = 200f;
    }
    public void NTHForce(){
        forceMagnitude = -200f;
    }
    // Update is called once per frame
    void Update()
    {
            // 获取输入框中的值
        // float inputForceMagnitude = 0f;
        // if (float.TryParse(forceMagnitudeInput.text, out inputForceMagnitude))
        // {
        //     // 如果能够成功解析文本为float类型，则将值赋给forceMagnitude
        //     forceMagnitude = inputForceMagnitude;
        // }
        // else
        // {
        //     // 如果解析失败，则显示错误信息
        //     Debug.Log("Invalid input value.");
        // }

        // 计算速度
        float angularAcceleration = forceMagnitude / armInertia;
        float angularVelocity = armState.y + angularAcceleration * Time.deltaTime; // 进行数值积分

        // 将速度应用于机械臂
        armState.y = angularVelocity;
        armTransform.eulerAngles = new Vector3(0f, 0f, armState.y); // 根据速度更新机械臂的旋转角度
        armRigidbody.angularVelocity = angularVelocity; // 应用角速度到Rigidbody组件
        // this.forceMagnitude = this.forceMagnitude * 0.9999f;

        //关于摩擦系数和时间帧率的速度衰减函数
        this.forceMagnitude *= (1f - frictionCoefficient * Time.deltaTime);
    }
}
