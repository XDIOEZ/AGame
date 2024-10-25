using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPoint : MonoBehaviour
{
    public GameObject blackCover;
    public GameObject UI;

    public bool isRecorded;
    public PlayerData_Temp player;



    private void Start()
    {
        // 获取场景中所有带有 "Player" 标签的物体
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        // 遍历这些物体并尝试获取 PlayerData_Temp 组件
        foreach (GameObject obj in playerObjects)
        {
            player = obj.GetComponent<PlayerData_Temp>();

            // 如果找到了第一个有 PlayerData_Temp 组件的物体，则停止遍历
            if (player != null)
            {
                break;
            }
        }

        // 如果遍历完成后 player 仍然为空，则输出警告信息
        if (player == null)
        {
            Debug.LogWarning("No PlayerData_Temp component found on any objects with the Player tag.");
        }

        //EventCenter.Instance.AddEventListener("PlayerDead", PlayerRemake);
    }



    private void Update()
    {
        
        if (isRecorded)
        {
            CheckDead();
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("进入触发器");
            StartCoroutine(nameof(TimerCountorCorotine));
            isRecorded = true;
        }

    }

    void CheckDead()
    {
       

        if (player.ifDead)
        {
            player.ifDead = false;
            StartCoroutine(nameof(TimeCountorCorotione2));
            
        }
    }


    void PlayerRemake()
    {
        player.ifDead = false;
        StartCoroutine(nameof(TimeCountorCorotione2));
    }
    IEnumerator TimerCountorCorotine()
    {
        Debug.Log("进入协程");
        UI.SetActive(true);
        yield return new  WaitForSeconds(3);
        Debug.Log("推出协程");
        UI.SetActive(false);
    }

    IEnumerator TimeCountorCorotione2()
    {
        blackCover.SetActive(true);
        player.transform.position = transform.position;
        player.ammo = player.lightEnergyLimation;
        player.health = 1;
        yield return new WaitForSeconds(1);
        blackCover.SetActive(false);
    }
}
