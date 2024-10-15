using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData_Temp : MonoBehaviour
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static PlayerData_Temp Instance { get; private set; }

    /// <summary>
    /// 玩家血量
    /// </summary>
    public int health = 100;

    /// <summary>
    /// 玩家弹药
    /// </summary>
    public int ammo = 4;

    void Awake()
    {
        /// <summary>
        /// 确保只有一个实例存在
        /// </summary>
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持对象在场景切换时不被销毁
        }
        else
        {
            Destroy(gameObject); // 如果已经有一个实例，销毁新的
        }
    }

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
