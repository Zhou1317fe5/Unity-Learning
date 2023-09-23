using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D; // ������һ��Rigidbody2D���͵ı���rigid2D�����ڿ��ƽ�ɫ�������˶���
    Animator animator; // ����Animator����

    float jumpForce = 780.0f; // ��Ծ��������С
    float walkForce = 15.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>(); //����ȡ������ͬһ��Ϸ�����ϵ�Rigidbody2D�����ֵ��rigid2D������
        this.animator = GetComponent<Animator>(); //����ǰ�����Animator��������ý���Animator����
    }
    // Update is called once per frame
    void Update()
    {



        //����������velocity.y=0
        //��Ծ
        //���Բ�����ʽ
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
            //�ֻ�������ʽ
        //if (Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce); // ����ɫʩ�ӳ��Ϸ������jumpForce����
        }

        //�����ƶ�
        int key = 0;
        //���Բ�����ʽ
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;
        //�ֻ�������ʽ
        //if (Input.acceleration.x > this.threshold) key = 1;
        //if (Input.acceleration.x < -this.threshold) key = -1;


        //��ɫ�ƶ��ٶ�
        float speedx = Mathf.Abs(this.rigid2D.velocity.x )*2;//* Time.deltaTime; // ��ȡ��ǰ��ɫ��ˮƽ�����ϵ��ٶ�ֵ����ȡ�����ֵ��
        //�����ٶ�
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        //ʹ��localScale�ı������ʵ���ֵ�Ϳ��Է�ת
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //���ݽ�ɫ�ƶ��ٶȸı䶯�������ٶ�
        this.animator.speed = speedx / 2.0f; // �����������ٶ����ƶ��ٶ���ϵ����


        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScenes");
        }

    }

    //����Ŀ�ĵ�
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("����Ŀ�ĵأ�");
        SceneManager.LoadScene("GameOverScene");
    }


}



