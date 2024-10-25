using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBackGround : MonoBehaviour
{
    [Header("ÎÞÏÞµØÍ¼")]
    public GameObject mainCamera;

    public float mapWidth;
    public float  mapNums;

    private float totalMapWidth;

    private void Start()
    {
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mapWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        totalMapWidth = mapWidth * mapNums;
    }

    private void Update()
    {
        Vector3 tempPos = transform.position;

        if (mainCamera.transform.position.x > transform.position.x + totalMapWidth / 2)
        {
            tempPos.x += totalMapWidth;
            transform.position = tempPos;
        }
        else if (mainCamera.transform.position.x < transform.position.x - totalMapWidth / 2)
        {
            tempPos.x -= totalMapWidth;
            transform.position = tempPos;
        }
    }

}
