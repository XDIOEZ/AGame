using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData_Temp : MonoBehaviour
{
    /// <summary>
    /// 玩家血量
    /// </summary>
    public int health = 100;

    /// <summary>
    /// 玩家弹药
    /// </summary>
    public int ammo ;
    // Start is called before the first frame update

    /// <summary>
    /// 玩家光能上限
    /// </summary>
    /// 
    public  int lightEnergyLimation=3;


    void Start()
    {
        ammo = lightEnergyLimation;
        Debug.Log("Player Health: " + health);
        Debug.Log("Player Ammo: " + ammo);
        Debug.Log("Player lightEnergyLimation:" + lightEnergyLimation);

        

    }




    // Update is called once per frame
    //void Update()
    //{
        
    //}

    /// <summary>
    /// 更改玩家的血量
    /// </summary>
    /// <param name="amount">要更改的血量值</param>
    public void ChangeHealth(int amount)
    {
        health += amount;
        Debug.Log("Updated Health: " + health);
    }

    /// <summary>
    /// 更改玩家的弹药
    /// </summary>
    /// <param name="amount">要更改的弹药值</param>
    public void ChangeAmmo(int amount)
    {
        if (amount>0)                   
        {
            if (ammo < lightEnergyLimation)//判断当前子弹数量是否小于光能上限
            {
                ammo += amount;
            }
 
            Debug.Log("Updated Ammo: " + ammo);
        }
        else if(amount<0)
        {
            if (ammo <= 0) return;
            ammo += amount;
            Debug.Log("Updated Ammo: " + ammo);
        }
       
    }
    public void ChangeLightEnergyLimation()
    {
        lightEnergyLimation++;
        Debug.Log("Player lightEnergyLimation:" + lightEnergyLimation);
    }
}
