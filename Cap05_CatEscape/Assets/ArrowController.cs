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
        //��֡�����½�
        transform.Translate(0, -0.02f, 0); // �ٶ���Ҫ���Ե�����
                                           //�������淶Χ�����ٶ���
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }
}
