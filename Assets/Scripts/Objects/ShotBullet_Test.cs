using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet_Test : MonoBehaviour
{
    public  GameObject light_Bullet;
    Rigidbody2D rb;
    PlayerMovement_Temp Player;
    float force=500f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GetComponent<PlayerMovement_Temp>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shot();
        }
    }

    private void Shot()
    {
         GameObject ga= Instantiate(light_Bullet, rb.position, Quaternion.identity);
        Light_Bullet bullet = ga.GetComponent<Light_Bullet>();
        bullet.Lunch(Player.lookDirection,force);
    }
}
