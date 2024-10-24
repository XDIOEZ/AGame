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
    /// <summary>
    /// 玩家死亡条件
    /// </summary>
    public bool ifDead;


    void Start()
    {
        ammo = lightEnergyLimation;
        Debug.Log("Player Health: " + health);
        Debug.Log("Player Ammo: " + ammo);
        Debug.Log("Player lightEnergyLimation:" + lightEnergyLimation);

        EventCenter.Instance.AddEventListener("KillPlayer", () =>
        {
            EventCenter.Instance.EventTrigger("PlayerDead");
            Debug.Log("Player Dead");
        });

    }
    /// <summary>
    /// 更改玩家的血量
    /// </summary>
    /// <param name="amount">要更改的血量值</param>
    public void ChangeHealth(int amount)
    {
        health += amount;
        Debug.Log("Updated Health: " + health);
        PlayerDeadCheck();

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
    public void PlayerDeadCheck()
    {
        Debug.Log("Player Dead");
        if (health <= 0)
        {
            ifDead = true;
            EventCenter.Instance.EventTrigger("PlayerDead");
        }
       
    }
    public void PlayerDeadOnce()
    {
        ifDead = true;
        Debug.Log("Player Dead");
        MusicMgr.Instance.PlaySound("OnDead", false);

        EventCenter.Instance.EventTrigger("PlayerDead");
    }
}
