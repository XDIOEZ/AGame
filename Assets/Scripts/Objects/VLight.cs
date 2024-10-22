using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VLight : MonoBehaviour
{
    public int energy = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerData_Temp pa = collision.GetComponent<PlayerData_Temp>();
            pa.ChangeAmmo(1);

            Destroy(gameObject);

        }

    }
}
