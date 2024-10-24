using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStarStone : MonoBehaviour
{
    
    PlayerData_Temp playerData;
    StarStone starStone;

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

               if (starStone.ga )    //���targetObjectû������ִ�����º���
                {
                    Destroy(starStone.ga);

                    playerData.ChangeLightEnergyLimation();

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
            starStone = collision.GetComponent<StarStone>();
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
