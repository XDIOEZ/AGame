using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPoint : MonoBehaviour
{
    public GameObject blackCover;
    public GameObject UI;

    public bool isRecorded;
    public PlayerData_Temp player;
    RecordPoint[] recordPoints;


    private void Start()
    {
        // ��ȡ���������д��� "Player" ��ǩ������
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");


        // ������Щ���岢���Ի�ȡ PlayerData_Temp ���
        foreach (GameObject obj in playerObjects)
        {
            player = obj.GetComponent<PlayerData_Temp>();

            // ����ҵ��˵�һ���� PlayerData_Temp ��������壬��ֹͣ����
            if (player != null)
            {
                break;
            }
        }

        //��ȡ���������д���RePoint�������RecordPoint���

        recordPoints = FindObjectsOfType<RecordPoint>();
        // ���������ɺ� player ��ȻΪ�գ������������Ϣ
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
            //���б������м�¼���isRecorded����Ϊfalse
            foreach (RecordPoint recordPoint in recordPoints)
            {
                recordPoint.isRecorded = false;
            }
            Debug.Log("���봥����");
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
        Debug.Log("����Э��");
        UI.SetActive(true);
        yield return new  WaitForSeconds(3);
        Debug.Log("�Ƴ�Э��");
        UI.SetActive(false);
    }

    IEnumerator TimeCountorCorotione2()
    {
        blackCover.SetActive(true);
        player.transform.position = transform.position;
        player.ammo = 5;
        player.health = 1;
        yield return new WaitForSeconds(1);
        blackCover.SetActive(false);
    }
}
