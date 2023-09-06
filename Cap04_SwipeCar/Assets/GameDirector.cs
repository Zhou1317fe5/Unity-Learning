using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 使用UI组件需要添加引用

public class GameDirector : MonoBehaviour
{
    GameObject car;
    GameObject flag;
    GameObject distance;
    // Start is called before the first frame update
    void Start()
    {
        this.car = GameObject.Find("car");
        this.flag = GameObject.Find("flag");
        this.distance = GameObject.Find("Distance");
    }

    // Update is called once per frame
    void Update()
    {
        float length = this.flag.transform.position.x - this.car.transform.position.x;
        if (length >= 0) { 
            this.distance.GetComponent<Text>().text = "距离目标" + length.ToString("F2") + "m"; 
        }

        else
        {
            this.distance.GetComponent<Text>().text = "游戏结束";
        }
        
    }
}
