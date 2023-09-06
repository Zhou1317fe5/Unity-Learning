using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 0;
    Vector2 startPos;
    Vector2 endPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //按下鼠标左键的坐标
            this.startPos = Input.mousePosition;
            GetComponent<AudioSource>().Play(); //播放音乐
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            float swipeLength = this.endPos.x - this.startPos.x;//最大可能值1920
            this.speed = swipeLength / 4000.0f;//效果不好，改4000
            Debug.Log(this.startPos);
            Debug.Log(this.endPos);
        }
        if (this.speed < 0.3f && this.speed > 0.01)
        {
            transform.Translate(this.speed, 0, 0); //移动
            this.speed *= 0.98f;
        }
    }
}
