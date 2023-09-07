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
        //��֡�����½�
        transform.Translate(0, -0.02f, 0); // �ٶ���Ҫ���Ե�����
                                           //�������淶Χ�����ٶ���
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
        // ��ײ���
        Vector2 p1 = transform.position;//ʹ��2ά����������ͷԲ������
        Vector2 p2 = this.player.transform.position;//��ɫԲ������
        Vector2 dir = p1 - p2;

        float d = dir.magnitude;//��ȡ�������ĳ��ȣ�ģ��
        float r1 = 0.2f;//��ͷԲ�뾶
        float r2 = 0.7f;//��ɫԲ�뾶

        
        if (d < r1 + r2)
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHP();
            Destroy(gameObject);
        }
    }
}
