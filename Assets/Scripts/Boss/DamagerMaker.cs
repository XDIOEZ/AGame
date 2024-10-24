using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerMaker : MonoBehaviour
{
    public int damage = 1; // �˺�ֵ
    [SerializeField]
    private PolygonCollider2D polygonCollider; // �������ײ������
    private bool CanDamaged = true; // ��־λ��׷���Ƿ��Ѿ��������ɹ��˺�
    private bool wasColliderEnabled = false; // ��¼��һ����ײ��������״̬

    // Start����Ϸ��ʼʱ����
    void Start()
    {
        // ��ȡ��ǰ�����ϵ�PolygonCollider2D���
        polygonCollider = GetComponent<PolygonCollider2D>();

        if (polygonCollider == null)
        {
            Debug.LogError("û���ҵ�PolygonCollider2D�����");
        }

        // ��ʼ����ײ��������״̬
        wasColliderEnabled = polygonCollider.enabled;
    }

    // Update��ÿ֡����
    void Update()
    {
        // ���������ײ��������״̬�Ƿ����仯
        if (polygonCollider != null && polygonCollider.enabled != wasColliderEnabled)
        {
            // �����ײ���Ӽ���״̬��Ϊʧ��״̬
            if (!polygonCollider.enabled)
            {
                // ����CanDamagedΪtrue
                CanDamaged = true;
            }

            // ���¼�¼����ײ��״̬
            wasColliderEnabled = polygonCollider.enabled;
        }

        // ���������ײ���Ƿ�������CanDamagedΪtrue
        if (polygonCollider != null && polygonCollider.enabled && CanDamaged)
        {
            // ����һ���Ӵ���������ֻ���"Player"�������
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Player"));

            // ���ڴ洢��⵽����ײ����б�
            List<Collider2D> results = new List<Collider2D>();

            // ���������ײ���ڵ���ײ���
            int hitCount = polygonCollider.OverlapCollider(filter, results);

            // �����⵽����ҵ���ײ
            if (hitCount > 0)
            {
                foreach (var collider in results)
                {
                    // ����ײ�������ϻ�ȡPlayerData_Temp���
                    PlayerData_Temp playerData = collider.GetComponent<PlayerData_Temp>();
                    if (playerData != null)
                    {
                        // ����ChangeHealth�������������˺�
                        playerData.ChangeHealth(-damage);
                        Debug.Log("�����������˺�: " + damage);

                        // ����CanDamagedΪfalse����ʾ�Ѿ��������ɹ��˺�
                        CanDamaged = false;
                        break;
                    }
                }
            }
        }
    }
}
