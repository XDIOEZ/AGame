using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // 导入 DOTween 的命名空间

public class PlayerDash_ : MonoBehaviour
{
    [Header("冲刺设置")]
    [SerializeField]
    private float dashDistance = 5f; // 冲刺的距离
    [SerializeField]
    private float dashDuration = 0.2f; // 冲刺持续的时间
    [SerializeField]
    private float dashCooldown = 1f; // 冲刺的冷却时间

    private bool canDash = true; // 是否可以冲刺
    private Tweener dashTweener; // 存储冲刺的 Tweener

    // 更新函数，监听冲刺输入
    void Update()
    {
        // 检测是否可以冲刺
        if (canDash)
        {
            Vector2 dashDirection = GetDashDirection();
            if (dashDirection != Vector2.zero)
            {
                Dash(dashDirection); // 执行冲刺
            }
        }
    }

    // 获取冲刺方向
    private Vector2 GetDashDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        return direction;
    }

    // 冲刺函数
    private void Dash(Vector2 dashDirection)
    {
        // 计算冲刺的目标位置
        Vector3 targetPosition = transform.position + (Vector3)dashDirection * dashDistance;

        // 开始冲刺移动
        dashTweener = transform.DOMove(targetPosition, dashDuration)
            .SetEase(Ease.OutQuad) // 使用缓动效果，使冲刺更流畅
            .OnComplete(() =>
            {
                // 冲刺完成后进入冷却状态
                StartCoroutine(DashCooldown());
            });

        // 禁止连续冲刺
        canDash = false;
    }

    // 冲刺冷却协程
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true; // 冷却完成后允许再次冲刺
    }

    // 停止冲刺的函数（如果需要）
    private void StopDash()
    {
        if (dashTweener != null && dashTweener.IsActive())
        {
            dashTweener.Kill(); // 停止当前的冲刺动画
        }
    }
}
