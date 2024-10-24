using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������˵��࣬ͨ����ײ�����������ҵ�����ֵ��
/// </summary>
public class PlayerDamagerChecker : MonoBehaviour
{
    [SerializeField]
    private PlayerData_Temp playerData; // ��������������

    // Start is called before the first frame update        
    void Start()
    {
        // ��ȡ������� PlayerData_Temp ���
        playerData = GetComponentInParent<PlayerData_Temp>();

        if (playerData == null)
        {
            Debug.LogError("�޷��ڸ��������ҵ� PlayerData_Temp �����");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ʹ�ô���������������壬�����˺�Դ
        if (other.CompareTag("DamageSource")) // �����˺�Դ�� Tag Ϊ "DamageSource"
        {
            float damageAmount = 10f; // ʾ���˺�ֵ
            TakeDamage(damageAmount);  // ���ô����˺��ķ���
            Debug.Log($"�������: -{damageAmount} ����ֵ: {playerData.health}"); // ���������Ϣ
        }
    }

    // �����˺��߼�
    private void TakeDamage(float damageAmount)
    {
        if (playerData != null)
        {
            playerData.ChangeHealth((int)damageAmount); // ���� PlayerData_Temp �� TakeDamage ����
            
            // ����Ƿ�����
            if (playerData.health <= 0)
            {
                Die();
            }
        }
    }

    // �����������
    private void Die()
    {
        Debug.Log("������������������߼���");
        // �����������������������߼������粥���������������ó�����
    }

    // Update is called once per frame
    void Update()
    {
        // �����Ҫ�����������ﴦ�������߼�
        //����-����������ֵ
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            TakeDamage(10f);
        }
    }
}
