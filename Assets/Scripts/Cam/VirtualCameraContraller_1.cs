using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraContraller_1 : MonoBehaviour
{
    public GameObject virtualCamera; // 挂接的虚拟摄像机

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否是玩家进入触发器
        if (other.CompareTag("Player"))
        {
            // 激活虚拟摄像机
            virtualCamera.SetActive(true);
            Debug.Log("虚拟摄像机已激活");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 检查是否是玩家退出触发器
        if (other.CompareTag("Player"))
        {
            // 禁用虚拟摄像机
            virtualCamera.SetActive(false);
            Debug.Log("虚拟摄像机已禁用");
        }
    }
}
