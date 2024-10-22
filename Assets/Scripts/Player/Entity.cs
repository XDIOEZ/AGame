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

    public EntityData Data; // 角色数据

    [SerializeField] public Rigidbody2D rb;// 2D刚体组件




    public float CurrentSpeed => rb.velocity.magnitude; // 使用表达式体简化属性
    public virtual void Start_()
    {
        // 确保刚体组件已经被分配
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
    public virtual void Update_()
    {
        debugDataShow.currentSpeed = CurrentSpeed;
    }
    // 方法：角色受伤
    public void TakeDamage(float damage)
    {
        Data.Health -= damage;
        if (Data.Health <= 0)
        {
            Die();
            Debug.Log(gameObject.name + " 已死亡");
        }
    }
    // 方法：角色死亡
    private void Die()
    {
        Debug.Log(gameObject.name + " 已死亡");
    }
    public void Move(Vector2 direction, float speed) // 通过 DoTween 移动到目标位置
    {
        Vector2 force = direction.normalized * speed * 100f;
        rb.AddForce(force, ForceMode2D.Impulse); // 使用冲击模式添加力
    }


}


[System.Serializable]
public class EntityData
{
    public float Health = 100f;   // 角色的血量
    public float MoveSpeed = 5f;  // 
}
