using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // ȷ���Ѿ���װ������DoTween��

public class TextMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private GameObject targetObject; // Ŀ��GameObject
    [SerializeField] private float moveDuration = 2f;   // �ƶ��ĳ���ʱ��
    [SerializeField] private Ease moveEase = Ease.Linear; // �ƶ��Ļ�������

    private Vector3 startPosition;  // ��ʼλ��
    private Vector3 targetPosition; // Ŀ��λ��

    private void OnValidate()
    {
        // �ڲ�������ʱ����Ŀ��λ��
        UpdateTargetPosition();
    }

    // ����Ŀ��λ��
    private void UpdateTargetPosition()
    {
        startPosition = transform.position;

        // ���Ŀ�����Ϊ�գ�����Ŀ��λ��ΪĿ������λ��
        if (targetObject != null)
        {
            targetPosition = targetObject.transform.position;
        }
        else
        {
            targetPosition = startPosition; // ���Ŀ�����Ϊ�գ�Ŀ��λ������Ϊ��ǰλ��
        }
    }

    // �ڱ༭ģʽ�Ͳ���ģʽ�»����ƶ��켣
    private void OnDrawGizmos()
    {
        UpdateTargetPosition();
        // ʹ��Gizmos�ڳ�����ͼ�л�����ɫ����
        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPosition, targetPosition);
    }

    // Start is called before the first frame update
    void Start()
    {
        // ȷ��Ŀ��������
        if (targetObject != null)
        {
            // ʹ��DoTweenƽ���ƶ���Ŀ������λ��
            transform.DOMove(targetPosition, moveDuration).SetEase(moveEase);
        }
        else
        {
            Debug.LogWarning("Ŀ�����δ����");
        }
    }
}
