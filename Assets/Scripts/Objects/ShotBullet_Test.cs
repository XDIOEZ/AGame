using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet_Test : MonoBehaviour
{


    public GameObject father; //获取父物体玩家以获取玩家上的组件
    public  GameObject light_Bullet;//获取水平方向的子弹预制体
    public GameObject light_Bullet_Up;//获取垂直方向上的子弹预制体

    Rigidbody2D rb;   //获取刚体以确定当前位置
    PlayerMovement_Temp Player;//获取Player Movement_Temp组件
    PlayerData_Temp bulletData;//获取PlayerData_Temp组件
    float force=500f;//发射子弹的力
    
    

    private void Awake()//在函数中实例化刚体与父物体玩家上的组件
    {
        rb = GetComponent<Rigidbody2D>();
        Player = father.GetComponent<PlayerMovement_Temp>();
        bulletData = father.GetComponent<PlayerData_Temp>();
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)&&bulletData.ammo>0)//按下J键时判断射击条件与射击角度
        {
            
            if (Input.GetKey(KeyCode.W))
            {
                bulletData.ChangeAmmo(-1);//调用相应函数并传递适当参数
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

    private void Shot()//水平射击
    {
         GameObject ga= Instantiate(light_Bullet, rb.position, Quaternion.identity);//函数被调用时生成以预制体light_Bullet为主体的物体ga
        Light_Bullet bullet = ga.GetComponent<Light_Bullet>();//获取ga上的Light_Bullet组件
        bullet.Lunch(Player.lookDirection,force);//调用Launch函数
    }
    private void ShotUp()//向上射击
    {
        GameObject ga = Instantiate(light_Bullet_Up, rb.position, Quaternion.identity);//同上
        Light_Bullet bullet = ga.GetComponent<Light_Bullet>();
        bullet.Lunch(new Vector2(0,1), force);
    }

    private void ShotDown()//向下射击
    {
        GameObject ga = Instantiate(light_Bullet_Up, rb.position, Quaternion.identity);
        Light_Bullet bullet = ga.GetComponent<Light_Bullet>();
        bullet.Lunch(new Vector2(0, -1), force);
    }
}
