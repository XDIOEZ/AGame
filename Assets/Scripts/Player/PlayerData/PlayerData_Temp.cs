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
    public int ammo = 4;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Health: " + health);
        Debug.Log("Player Ammo: " + ammo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        ammo += amount;
        Debug.Log("Updated Ammo: " + ammo);
    }
}
