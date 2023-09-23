using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D; // 声明了一个Rigidbody2D类型的变量rigid2D，用于控制角色的物理运动。
    Animator animator; // 声明Animator变量

    float jumpForce = 780.0f; // 跳跃的力量大小
    float walkForce = 15.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>(); //将获取附加在同一游戏对象上的Rigidbody2D组件赋值给rigid2D变量。
        this.animator = GetComponent<Animator>(); //将当前对象的Animator组件的引用交给Animator变量
    }
    // Update is called once per frame
    void Update()
    {



        //起跳条件，velocity.y=0
        //跳跃
        //电脑操作方式
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
            //手机操作方式
        //if (Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce); // 给角色施加朝上方向乘以jumpForce的力
        }

        //左右移动
        int key = 0;
        //电脑操作方式
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;
        //手机操作方式
        //if (Input.acceleration.x > this.threshold) key = 1;
        //if (Input.acceleration.x < -this.threshold) key = -1;


        //角色移动速度
        float speedx = Mathf.Abs(this.rigid2D.velocity.x )*2;//* Time.deltaTime; // 获取当前角色在水平方向上的速度值，并取其绝对值。
        //限制速度
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        //使用localScale改变缩放率到负值就可以反转
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //根据角色移动速度改变动画播放速度
        this.animator.speed = speedx / 2.0f; // 将动画播放速度与移动速度联系起来


        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScenes");
        }

    }

    //到达目的地
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("到达目的地！");
        SceneManager.LoadScene("GameOverScene");
    }


}



