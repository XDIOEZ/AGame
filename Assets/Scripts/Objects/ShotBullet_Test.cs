using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet_Test : MonoBehaviour
{


    public GameObject father; //��ȡ����������Ի�ȡ����ϵ����
    public  GameObject light_Bullet;//��ȡˮƽ������ӵ�Ԥ����
    public GameObject light_Bullet_Up;//��ȡ��ֱ�����ϵ��ӵ�Ԥ����

    Rigidbody2D rb;   //��ȡ������ȷ����ǰλ��
    PlayerMove Player;//��ȡPlayer Movement_Temp���
    Vector2 Bulletdirection; //�ӵ�����
    PlayerData_Temp bulletData;//��ȡPlayerData_Temp���
    float force=500f;//�����ӵ�����

    public string OnShoot = "OnShoot";
    

    private void Awake()//�ں�����ʵ���������븸��������ϵ����
    {
        rb = GetComponent<Rigidbody2D>();
        Player = father.GetComponent<PlayerMove>();
        bulletData = father.GetComponent<PlayerData_Temp>();

        EventCenter.Instance.AddEventListener<object>("PlayerDirectionChanged", (object obj) =>
        {
            PlayerMove move = obj as PlayerMove; // �� obj ת��Ϊ PlayerMove
            if (move != null)
            {
                Debug.Log(move.PlayerDirection);

                Bulletdirection = new Vector2(move.PlayerDirection, 0);
            }
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)&&bulletData.ammo>0)//����J��ʱ�ж��������������Ƕ�
        {
            MusicMgr.Instance.PlaySound(OnShoot, false);
            
            if (Input.GetKey(KeyCode.W))
            {
                bulletData.ChangeAmmo(-1);//������Ӧ�����������ʵ�����
                ShotUp();
                
            }
            else if (Input.GetKey(KeyCode.S))
            {
                bulletData.ChangeAmmo(-1);
                ShotDown();
                
                
            }
            else
            {
                bulletData.ChangeAmmo(-1);
                Shot();
               
            }

        }
    }


    private void Shot()//ˮƽ���
    {
        GameObject ga = Instantiate(light_Bullet, rb.position, Quaternion.identity); // ������Ԥ���� light_Bullet Ϊ��������� ga
        Light_Bullet bullet = ga.GetComponent<Light_Bullet>(); // ��ȡ ga �ϵ� Light_Bullet ���
        bullet.Lunch(Bulletdirection, force); // ���� Lunch ����
    }
    private void ShotUp()//�������
    {
        GameObject ga = Instantiate(light_Bullet_Up, rb.position, Quaternion.identity);//ͬ��
        Light_Bullet bullet = ga.GetComponent<Light_Bullet>();
        bullet.Lunch(new Vector2(0,1), force);
    }

    private void ShotDown()//�������
    {
        GameObject ga = Instantiate(light_Bullet_Up, rb.position, Quaternion.identity);
        Light_Bullet bullet = ga.GetComponent<Light_Bullet>();
        bullet.Lunch(new Vector2(0, -1), force);
    }
}
