using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    // ��ȡ�ҽӶ���� Entity ���
    public Entity entity;
    // AI ���ƶ�Ŀ��
    public Transform enemyTargetPosition;
    // AI �Ĺ���Ŀ��
    public Transform attackTargetPosition;
    // AI ��ǰ��λ��
    public Transform currentPosition;

    public int MaxSkillInput;

    private void Start()
    {
        // ���õ�ǰλ��Ϊ����ĳ�ʼλ��
        currentPosition = transform;
        //Debug.Log("��ʼ�� AI ��ǰλ�ã�" + currentPosition.position);
    }
}
