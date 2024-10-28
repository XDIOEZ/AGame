using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 需要引入这个命名空间以使用场景管理功能

public class DEBUGCONSOlE : MonoBehaviour
{
    // 单例且不在Unity销毁
    private static DEBUGCONSOlE instance;
    private string debugInfo = ""; // 调试信息

    bool isDebug = false; // 是否处于调试模式
    bool setJumpCount = false; // 用于控制是否每帧设置跳跃计数

    void Start()
    {
        // 确保只有一个DEBUGCONSOlE实例存在
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 切换场景时不销毁
        }
        else
        {
            Destroy(gameObject); // 如果已经有实例存在，销毁新的实例
        }
    }

    void Update()
    {
        // 按下F12启动控制台
        if (Input.GetKeyDown(KeyCode.F12))
        {
            isDebug = !isDebug; // 切换调试模式
        }

        if (!isDebug)
        {
            return; // 如果不在调试模式，直接返回
        }

        // 切换场景逻辑
        for (int i = 0; i <= 5; i++)
        {
            if (Input.GetKeyDown((KeyCode)(KeyCode.Alpha0 + i)))
            {
                SceneManager.LoadScene(i); // 加载对应场景
            }
        }

        // 按下F11清空debugInfo
        if (Input.GetKeyDown(KeyCode.F11))
        {
            debugInfo = ""; // 清空调试信息
        }

        // 获取玩家数据（按下G键）
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // 查找所有玩家对象
            if (players.Length > 0)
            {
                foreach (GameObject player in players)
                {
                    PlayerData_Temp playerData = player.GetComponent<PlayerData_Temp>();
                    PlayerMove playerMove = player.GetComponent<PlayerMove>();

                    // 检查组件并记录信息
                    if (playerData != null && playerMove != null)
                    {
                        debugInfo += $"玩家找到 - 生命值: {playerData.health}, 弹药: {playerData.ammo}, 光能: {playerData.lightEnergyLimation}, 跳跃计数: {playerMove.jumpCount}\n";
                    }
                    else
                    {
                        if (playerData == null)
                        {
                            debugInfo += $"玩家对象 {player.name} 缺少 PlayerData_Temp 组件。\n";
                        }
                        if (playerMove == null)
                        {
                            debugInfo += $"玩家对象 {player.name} 缺少 PlayerMove 组件。\n";
                        }
                    }
                }
            }
            else
            {
                debugInfo += "场景中未找到玩家对象。\n";
            }
        }

        // 设置玩家数据（按下F8键）
        if (Input.GetKeyDown(KeyCode.F8))
        {
            setJumpCount = true; // 设置跳跃计数的标志为true
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // 查找所有玩家对象
            if (players.Length > 0)
            {
                foreach (GameObject player in players)
                {
                    PlayerData_Temp playerData = player.GetComponent<PlayerData_Temp>();
                    PlayerMove playerMove = player.GetComponent<PlayerMove>();

                    // 设置 PlayerData_Temp 属性
                    if (playerData != null)
                    {
                        playerData.health = 99; // 设置生命值为99
                        playerData.ammo = 99;   // 设置弹药为99
                        playerData.lightEnergyLimation = 99; // 设置光能为99
                        debugInfo += $"玩家 {player.name} 属性设置为99（生命值、弹药、光能）。\n";
                    }
                    else
                    {
                        debugInfo += $"玩家对象 {player.name} 缺少 PlayerData_Temp 组件。\n";
                    }

                    // 设置 PlayerMove 属性
                    if (playerMove != null)
                    {
                        playerMove.jumpCount = 99; // 设置跳跃计数为99
                        debugInfo += $"玩家 {player.name} 的跳跃计数设置为99。\n"; // 更新调试信息
                    }
                    else
                    {
                        debugInfo += $"玩家对象 {player.name} 缺少 PlayerMove 组件。\n";
                    }
                }
            }
            else
            {
                debugInfo += "场景中未找到玩家对象。\n";
            }
        }

        // 每帧设置跳跃计数为99
        if (setJumpCount)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // 查找所有玩家对象
            foreach (GameObject player in players)
            {
                PlayerMove playerMove = player.GetComponent<PlayerMove>();
                if (playerMove != null)
                {
                    playerMove.jumpCount = 99; // 每帧设置跳跃计数为99
                }
            }
        }
    }

    void OnGUI()
    {
        if (isDebug)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 24;
            style.normal.textColor = Color.white;

            // 显示调试模式激活信息
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 50), "调试模式已开启", style);

            // 显示功能描述
            GUI.Label(new Rect(10, 10, 500, 50), "按F12切换调试模式。", style);
            GUI.Label(new Rect(10, 40, 500, 50), "按0-5切换场景。", style);
            GUI.Label(new Rect(10, 70, 500, 50), "按G获取玩家数据。", style);
            GUI.Label(new Rect(10, 100, 500, 50), "按F8将生命值、弹药和光能设置为99，并每帧设置跳跃计数为99。", style);
            GUI.Label(new Rect(10, 130, 500, 50), "按F11清空调试信息。", style);
            GUI.Label(new Rect(10, 170, 500, 50), debugInfo, style); // 显示调试信息
        }
    }
}
