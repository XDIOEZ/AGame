using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStarStone : MonoBehaviour
{
    
    PlayerData_Temp playerData;
    StarStone starStone;

    //判定条件
    [SerializeField] bool isInArea;//判断是否到达星石的判断区域

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

               if (starStone.ga )    //如果targetObject没有销毁执行以下函数
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
