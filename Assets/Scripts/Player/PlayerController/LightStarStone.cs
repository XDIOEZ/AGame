using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStarStone : MonoBehaviour
{
    public GameObject targetObject;//��ȡ���壬����Ϊ��ʯ�ϵ�Canvas���
    PlayerData_Temp playerData;

    //�ж�����
    [SerializeField] bool isInArea;//�ж��Ƿ񵽴���ʯ���ж�����

    private void Awake()
    {
        playerData = GetComponent<PlayerData_Temp>();
    }



    private void Update()
    {
        if (isInArea == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {

               if (targetObject != null)    //���targetObjectû������ִ�����º���
                {
                    playerData.ChangeLightEnergyLimation();
                    Destroy(targetObject);
                    playerData.ammo = playerData.lightEnergyLimation;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StarStone"))
        {
            isInArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("StarStone"))
        {
            isInArea = false;
        }
    }


}
