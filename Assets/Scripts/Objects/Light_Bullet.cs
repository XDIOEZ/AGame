using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    public void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TriLens"))
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
}
