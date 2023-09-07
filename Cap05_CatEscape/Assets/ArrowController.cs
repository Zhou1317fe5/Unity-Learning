using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        //逐帧匀速下降
        transform.Translate(0, -0.02f, 0); // 速度需要调试到合适
                                           //超出画面范围则销毁对象
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
        // 碰撞检测
        Vector2 p1 = transform.position;//使用2维向量表征箭头圆心坐标
        Vector2 p2 = this.player.transform.position;//角色圆心坐标
        Vector2 dir = p1 - p2;

        float d = dir.magnitude;//获取该向量的长度（模）
        float r1 = 0.2f;//箭头圆半径
        float r2 = 0.7f;//角色圆半径

        
        if (d < r1 + r2)
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHP();
            Destroy(gameObject);
        }
    }
}
