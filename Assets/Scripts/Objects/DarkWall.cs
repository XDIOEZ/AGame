using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerData_Temp>().ChangeHealth(-1);
        }
        if (collision.CompareTag("Light_Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
