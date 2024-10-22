using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet_Test : MonoBehaviour
{


    public GameObject father; //��ȡ����������Ի�ȡ����ϵ����
    public  GameObject light_Bullet;//��ȡˮƽ������ӵ�Ԥ����
    public GameObject light_Bullet_Up;//��ȡ��ֱ�����ϵ��ӵ�Ԥ����

    Rigidbody2D rb;   //��ȡ������ȷ����ǰλ��
    PlayerMovement_Temp Player;//��ȡPlayer Movement_Temp���
    PlayerData_Temp bulletData;//��ȡPlayerData_Temp���
    float force=500f;//�����ӵ�����
    
    

    private void Awake()//�ں�����ʵ���������븸��������ϵ����
    {
        rb = GetComponent<Rigidbody2D>();
        Player = father.GetComponent<PlayerMovement_Temp>();
        bulletData = father.GetComponent<PlayerData_Temp>();
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)&&bulletData.ammo>0)//����J��ʱ�ж��������������Ƕ�
        {
            
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
         GameObject ga= Instantiate(light_Bullet, rb.position, Quaternion.identity);//����������ʱ������Ԥ����light_BulletΪ���������ga
        Light_Bullet bullet = ga.GetComponent<Light_Bullet>();//��ȡga�ϵ�Light_Bullet���
        bullet.Lunch(Player.lookDirection,force);//����Launch����
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
