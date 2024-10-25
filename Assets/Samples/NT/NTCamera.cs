using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTCamera : MonoBehaviour
{
    public float smoothSpeed = 0.125f; // 相机平滑跟随的速度
    public Camera mainCamera; // 主相机
    public List<Transform> backgroundLayers = new List<Transform>(); // 背景层列表
    public float[] backgroundParallaxFactors; // 每个背景层的视差因子

    private Transform player; // 玩家节点的 Transform

    void Start()
    {
        // 设置主摄像机
        if (mainCamera == null)
            mainCamera = Camera.main;

        // 设置相机的初始偏移
        StartCoroutine(UpdatePlayerPosition());

        // 初始化背景视差因子
        backgroundParallaxFactors = new float[backgroundLayers.Count];
        for (int i = 0; i < backgroundLayers.Count; i++)
        {
            backgroundParallaxFactors[i] = 1f / (i + 1); // 根据层级设置视差因子，越远的层级因子越小
        }
    }

    void Update()
    {
        if (player == null)
            return;

        // 相机跟随玩家
        Vector3 desiredPosition = player.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 处理背景层的视差运动
        MoveBackgroundLayers();
    }

    // 【协程】轮询玩家节点，用于相机追踪
    IEnumerator UpdatePlayerPosition()
    {
        while (player == null)
        {
            player =
                GameObject.FindGameObjectWithTag("Player")?.transform
                ?? GameObject.Find("Eye")?.transform;
            yield return new WaitForSeconds(0.2f);
        }
    }

    void MoveBackgroundLayers()
    {
        // 背景层的同向运动
        for (int i = 0; i < backgroundLayers.Count; i++)
        {
            Vector3 backgroundPosition = backgroundLayers[i].position;
            backgroundPosition.x = player.position.x * backgroundParallaxFactors[i]; // 根据视差因子调整背景层位置
            backgroundLayers[i].position = backgroundPosition;
        }
    }
}
