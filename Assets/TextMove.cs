using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // 确保已经安装并导入DoTween库

public class TextMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private GameObject targetObject; // 目标GameObject
    [SerializeField] private float moveDuration = 2f;   // 移动的持续时间
    [SerializeField] private Ease moveEase = Ease.Linear; // 移动的缓动类型

    private Vector3 startPosition;  // 初始位置
    private Vector3 targetPosition; // 目标位置

    private void OnValidate()
    {
        // 在参数更改时更新目标位置
        UpdateTargetPosition();
    }

    // 计算目标位置
    private void UpdateTargetPosition()
    {
        startPosition = transform.position;

        // 如果目标对象不为空，更新目标位置为目标对象的位置
        if (targetObject != null)
        {
            targetPosition = targetObject.transform.position;
        }
        else
        {
            targetPosition = startPosition; // 如果目标对象为空，目标位置设置为当前位置
        }
    }

    // 在编辑模式和播放模式下绘制移动轨迹
    private void OnDrawGizmos()
    {
        UpdateTargetPosition();
        // 使用Gizmos在场景视图中绘制绿色线条
        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPosition, targetPosition);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 确保目标对象存在
        if (targetObject != null)
        {
            // 使用DoTween平滑移动到目标对象的位置
            transform.DOMove(targetPosition, moveDuration).SetEase(moveEase);
        }
        else
        {
            Debug.LogWarning("目标对象未设置");
        }
    }
}
