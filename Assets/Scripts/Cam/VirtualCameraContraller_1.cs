using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraContraller_1 : MonoBehaviour
{
    public GameObject virtualCamera; // �ҽӵ����������

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ����Ƿ�����ҽ��봥����
        if (other.CompareTag("Player"))
        {
            // �������������
            virtualCamera.SetActive(true);
            Debug.Log("����������Ѽ���");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ����Ƿ�������˳�������
        if (other.CompareTag("Player"))
        {
            // �������������
            virtualCamera.SetActive(false);
            Debug.Log("����������ѽ���");
        }
    }
}
