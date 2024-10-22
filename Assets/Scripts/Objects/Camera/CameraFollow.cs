using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public GameObject backGround1;
    public GameObject backGround2;
    


    void Update()
    {
        if (target)
        {
            transform.position = target.position;
        }
    }
}
