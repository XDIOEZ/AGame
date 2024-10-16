using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VLight : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerData_Temp>().ChangeAmmo(1);

            Destroy(gameObject);

        }

    }
}
