using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPoint : MonoBehaviour
{
    public GameObject blackCover;
    public GameObject UI;

    public bool isRecorded;
    PlayerData_Temp player;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData_Temp>();
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
        yield return new WaitForSeconds(1);
        blackCover.SetActive(false);
    }
}
