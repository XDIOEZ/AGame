using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    // 获取挂接对象的 Entity 组件
    public Entity entity;
    // AI 的移动目标
    public Transform enemyTargetPosition;
    // AI 的攻击目标
    public Transform attackTargetPosition;
    // AI 当前的位置
    public Transform currentPosition;

    public int MaxSkillInput;

    private void Start()
    {
        // 设置当前位置为对象的初始位置
        currentPosition = transform;
        //Debug.Log("初始化 AI 当前位置：" + currentPosition.position);
    }
}
