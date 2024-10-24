using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalTrigger : MonoBehaviour
{
    public bool Right;
    public bool Left;
    public bool Up;
    public bool Down;

    public Crystalreflection Praent;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Right)
            { Praent.ReBoundPlayer(1); }
            else if (Left)
            { Praent.ReBoundPlayer(2); }
            else if (Up)
            { Praent.ReBoundPlayer(3); }
            else 
            { Praent.ReBoundPlayer(4); }

        }
    }
}
