using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateMachineBehaviour
{
    [Header("等待时间&&计时器")]
    [SerializeField]
    private float waitTimer = 3f; // 等待时间
    [SerializeField]
    private float timer; // 计时器
    [Header("状态切换设置")]
    [SerializeField]
    private  string EndAction; // 移动动作

    // 进入状态时重置计时器
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f; // 重置计时器
    }

    // 在每帧更新中检查等待时间
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime; // 更新计时器

        // 如果等待时间达到设定值，切换到 Move 动画
        if (timer >= waitTimer && EndAction!= "")
        {
            animator.SetTrigger(EndAction);
        }
    }

    // 离开状态时清除定时器
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f; // 重置计时器
    }
}
