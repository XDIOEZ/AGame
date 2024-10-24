using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PotalSetting : MonoBehaviour
{
    public Transform trans;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&collision.GetComponent<PlayerData_Temp>().ammo>0)
        {
            collision.transform.position = trans.position;
        }
    }
}
