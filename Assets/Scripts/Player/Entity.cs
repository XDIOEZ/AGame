using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDebugDataShow
{
    public float currentSpeed;
}
public class Entity : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] PlayerDebugDataShow debugDataShow = new PlayerDebugDataShow();

    public EntityData Data; // ��ɫ����

    [SerializeField] public Rigidbody2D rb;// 2D�������




    public float CurrentSpeed => rb.velocity.magnitude; // ʹ�ñ��ʽ�������
    public virtual void Start_()
    {
        // ȷ����������Ѿ�������
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
    public virtual void Update_()
    {
        debugDataShow.currentSpeed = CurrentSpeed;
    }
    // ��������ɫ����
    public void TakeDamage(float damage)
    {
        Data.Health -= damage;
        if (Data.Health <= 0)
        {
            Die();
            Debug.Log(gameObject.name + " ������");
        }
    }
    // ��������ɫ����
    private void Die()
    {
        Debug.Log(gameObject.name + " ������");
    }
    public void Move(Vector2 direction, float speed) // ͨ�� DoTween �ƶ���Ŀ��λ��
    {
        Vector2 force = direction.normalized * speed * 100f;
        rb.AddForce(force, ForceMode2D.Impulse); // ʹ�ó��ģʽ�����
    }


}


[System.Serializable]
public class EntityData
{
    public float Health = 100f;   // ��ɫ��Ѫ��
    public float MoveSpeed = 5f;  // 
}
