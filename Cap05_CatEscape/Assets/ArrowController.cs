using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
