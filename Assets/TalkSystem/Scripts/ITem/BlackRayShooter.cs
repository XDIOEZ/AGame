using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackRayShooter : MonoBehaviour
{
    public float maxLength = 10.0f; // 最大长度
    public float lengthIncreaseSpeed = 1.0f; // 长度增加速度
    public float damage = 10.0f; // 碰撞到玩家造成的伤害
    public LayerMask playerLayer; // 玩家的碰撞层


    public ParticleSystem effct;
    private LineRenderer lineRenderer;
    private float currentLength = 0.0f;

    void Start()
    {
        // 获取LineRenderer组件
        lineRenderer = GetComponent<LineRenderer>();

        // 设置线的起始和结束位置
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.startWidth = 0.2f;
    }

    void Update()
    {
        // 更新线的长度
        currentLength += lengthIncreaseSpeed * Time.deltaTime;
        Vector3 endPosition = transform.position + transform.right * currentLength;
        lineRenderer.SetPosition(1, endPosition);


        effct.transform.position = endPosition;
        // 如果长度超过最大长度，停止线
        if (currentLength > maxLength)
        {
            Destroy(gameObject);
        }

        Ray ray = new Ray(transform.position, transform.right); // 从物体的位置向右发射射线
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, currentLength, playerLayer); ;

        // 检测碰撞
        if (hit.collider != null)
        {
            // 如果射线与物体碰撞，打印碰撞信息
            Debug.Log("Hit " + hit.collider.name);

            // 如果碰撞到玩家，造成伤害
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                // hit.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
                Destroy(hit.collider.gameObject);
                Destroy(gameObject);
            }
        }
    }
}