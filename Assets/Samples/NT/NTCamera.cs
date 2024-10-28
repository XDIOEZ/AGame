using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class NTCamera : MonoBehaviour
{
    public float bgWidth = 19.2f;
    public float bgHeight = 10.8f;
    public float smoothSpeed = 0.125f;
    public Camera mainCamera;
    public List<Transform> backgroundLayers = new List<Transform>();
    public float[] backgroundParallaxFactors;
    // private Transform player;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        // StartCoroutine(UpdatePlayerPosition());

        backgroundParallaxFactors = new float[backgroundLayers.Count];
        for (int i = 0; i < backgroundLayers.Count; i++)
        {
            backgroundParallaxFactors[i] = 1f / (i + 1);
        }
    }

    void Update()
    {
        // if (player == null)
        //     return;

        // Vector3 desiredPosition = player.position;
        // Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // transform.position = smoothedPosition;

        MoveBackgroundLayers();
    }

    // IEnumerator UpdatePlayerPosition()
    // {
    //     while (player == null)
    //     {
    //         player =
    //             GameObject.FindGameObjectWithTag("Player")?.transform
    //             ?? GameObject.Find("Eye")?.transform;
    //         yield return new WaitForSeconds(0.2f);
    //     }
    // }

    void MoveBackgroundLayers(Vector3 deltaPosition = new Vector3())
    {
        for (int i = 0; i < backgroundLayers.Count; i++)
        {
            Vector3 offsetPosition = new Vector3(
                transform.position.x * backgroundParallaxFactors[i] % bgWidth,
                transform.position.y * backgroundParallaxFactors[i] % bgHeight,
                -1f
            );
            backgroundLayers[i].localPosition = -offsetPosition;
        }
    }
}
