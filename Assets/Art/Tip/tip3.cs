using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tip3 : MonoBehaviour
{
    public PlayerMove playerMove;

    void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.D)
        )
        {
            gameObject.SetActive(false);
            playerMove.enabled = true;
        }
    }
}
