using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet_Test : MonoBehaviour
{


    public GameObject father; //获取父物体玩家以获取玩家上的组件
    public  GameObject light_Bullet;//获取水平方向的子弹预制体
    public GameObject light_Bullet_Up;//获取垂直方向上的子弹预制体

    Rigidbody2D rb;   //获取刚体以确定当前位置
    PlayerMove Player;//获取Player Movement_Temp组件
    Vector2 Bulletdirection; //子弹方向
    PlayerData_Temp bulletData;//获取PlayerData_Temp组件
    float force=500f;//发射子弹的力

    public string OnShoot = "OnShoot";
    

    private void Awake()//在函数中实例化刚体与父物体玩家上的组件
    {
        rb = GetComponent<Rigidbody2D>();
        Player = father.GetComponent<PlayerMove>();
        bulletData = father.GetComponent<PlayerData_Temp>();

        EventCenter.Instance.AddEventListener<object>("PlayerDirectionChanged", (object obj) =>
        {
            PlayerMove move = obj as PlayerMove; // 将 obj 转换为 PlayerMove
            if (move != null)
            {
                Debug.Log(move.PlayerDirection);

                Bulletdirection = new Vector2(move.PlayerDirection, 0);
            }
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)&&bulletData.ammo>0)//按下J键时判断射击条件与射击角度
        {
            MusicMgr.Instance.PlaySound(OnShoot, false);
            
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
        GameObject ga = Instantiate(light_Bullet, rb.position, Quaternion.identity); // 生成以预制体 light_Bullet 为主体的物体 ga
        Light_Bullet bullet = ga.GetComponent<Light_Bullet>(); // 获取 ga 上的 Light_Bullet 组件
        bullet.Lunch(Bulletdirection, force); // 调用 Lunch 函数
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
